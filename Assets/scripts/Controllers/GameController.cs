using System;
using System.Collections.Generic;
using UnityEngine;



public class GameController : MonoBehaviour
{
    [Header("------HUD------")]
    [Space]
    public UIManager manager;
    private HeroManager heroManager;
    private EnemyManager enemyManager;
    [Header("------Game Objects------")]
    [Space]
    public Creature selectObject;
    public Creature targerObject;

    [Header("------Lists------")]
    [Space]
    public List<Creature> fightList;
    public List<Hero> playerList;
    public List<Enemy> enemyList;

    [Header("------System info------")]
    [Space]
    public bool isBossFight = false;
    private bool isTurnEnd = false;
    private bool isGameEnd = false;

    [Header("------Actions------")]
    [Space]
    public static Action OnKilledAllEnemies;
    public static Action<Hero> OnKilledHero;
    public static Action<Enemy> OnKilledEnemy;


    public Creature GetActiveCreature { get { return fightList[0]; } }

    private void OnEnable()
    {
        EnemyManager.OnEnemyAdded += AddEnemyToList;
        HeroManager.OnHeroAdded += AddHeroToList;
    }

    private void OnDisable()
    {
        EnemyManager.OnEnemyAdded -= AddEnemyToList;
        HeroManager.OnHeroAdded -= AddHeroToList;
    }


    private void Start()
    {
        manager = GetComponent<UIManager>();
        heroManager = GetComponent<HeroManager>();
        enemyManager = GetComponent<EnemyManager>();

    }

    private void AddHeroToList(Hero hero)
    {
        playerList.Add(hero);
        fightList.Add(hero);
    }

    private void AddEnemyToList(Enemy enemy)
    {
        enemyList.Add(enemy);
        fightList.Add(enemy);
    }


    private void FixedUpdate()
    {
        foreach (var item in playerList)
        {
            var moveAmount = heroManager.heroSpeed * Time.deltaTime;

            item.transform.position += Vector3.right * moveAmount;
        }
    }

    private void Update()
    {

        if (isGameEnd)
        {
            return;
        }

        if (IsPlayerTurn())
        {
            if(selectObject == null)
            {
                SelectActiveObject();
            }
            if (!isTurnEnd && Input.GetMouseButtonDown(0))
            {
              SelectTarget();
            }
        }
        else
        {
            if (!isTurnEnd)
            {
                isTurnEnd = true;
                SelectActiveObject();
                EnemyTurnProccesing();
            }
        }
    }

    private void EnemyTurnProccesing()
    {
        targerObject = selectObject.GetComponent<Enemy>().ChooseSpellAndGetTarget();

        if (targerObject.TryGetComponent<Hero>(out _))
        {
            manager.ShowTargetCircle();
            manager.SetTargetCirclePosition(targerObject.transform.position);
        }
        else
        {
            manager.ShowTeamTargetCircle();
            manager.SetTeamTargetCirclePosition(targerObject.transform.position);
        }

        Invoke("EnemyTurnEnd", 1.5f);
    }

    private void EnemyTurnEnd()
    {    
        selectObject.GetComponent<Enemy>().EnemyTurn();

        RemovePlayerWhenNoHp();
        EndTurn();
    }

    private void RemovePlayerWhenNoHp()
    {
        if (targerObject.health <= 0)
        {
            manager.HideTargetCircle();
            OnKilledHero?.Invoke(targerObject.GetComponent<Hero>());
            playerList.Remove(targerObject.GetComponent<Hero>());
            fightList.Remove(targerObject.GetComponent<Hero>());
        }
    }

    public void UseCreatureSpell()
    {
        if (selectObject != null && targerObject != null && !isTurnEnd)
        {
            selectObject.GetComponent<CastSpellController>().UseSpell(targerObject);
            RemoveEnemyWhenNoHp();
            isTurnEnd = true;
            EndTurn();
        }
    }

    private void RemoveEnemyWhenNoHp()
    {
        if (targerObject.health <= 0)
        {
            selectObject.GetComponent<Hero>().AddExp(200);
            selectObject.GetComponent<Hero>().LevelUp();
            manager.HideTargetCircle();
            OnKilledEnemy?.Invoke(targerObject?.GetComponent<Enemy>());
            enemyList.Remove(targerObject.GetComponent<Enemy>());
            fightList.Remove(targerObject.GetComponent<Enemy>());
        }
    }

    private void SelectActiveObject()
    {

        selectObject = GetActiveCreature;
        if (selectObject != null)
        {
            manager.ShowSelectCircle();
            manager.SetSelectCirclePosition(selectObject.transform.position);
            NewTurnProccesing();
        }

    }

    private void NewTurnProccesing()
    {
        if (IsPlayerTurn())
        {
            selectObject.AddWeaponSpellsToHeroSpells();
            manager.ShowHUD();
            manager.ReloadSpellHUD();
        }
        else
        {
            manager.HideHUD();
        }
        selectObject.ActivateStates();
    }

    private void SelectTarget()
    {
        FindObjectOfType<ColliderCreationController>().DeleteCollider();
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        var csc = selectObject.GetComponent<CastSpellController>();
       

        if (csc.isSpellSelected && csc.CanCastForEnemy() && hit.collider?.tag == "Enemy")
        {
            SelectTargetProccesing(hit, csc.CanCastForEnemy());
        }
        else if(csc.isSpellSelected && !csc.CanCastForEnemy() && hit.collider?.tag == "Player")
        {
            SelectTargetProccesing(hit, csc.CanCastForEnemy());
        }


    }

    private void SelectTargetProccesing(RaycastHit2D hit, bool castForEnemy)
    {
        targerObject = hit.collider.GetComponent<Creature>();
        if (targerObject != null)
        {
            if (castForEnemy)
            {
                manager.ShowTargetCircle();
                manager.HideTeamTargetCircle();
                manager.SetTargetCirclePosition(hit.collider.transform.position);
            }
            else 
            {
                manager.ShowTeamTargetCircle();
                manager.HideTargetCircle();
                manager.SetTeamTargetCirclePosition(hit.collider.transform.position);
            }
        }
    }

    private bool IsPlayerTurn()
    {
        return GetActiveCreature.GetComponent<Hero>() != null;
    }

    private void EndTurn()
    {
        EndMoveSettings();
        EndGameProcessing();
    }

    private void EndMoveSettings()
    {
        fightList.Add(GetActiveCreature);
        fightList.RemoveAt(0);

        selectObject = null;
        targerObject = null;

        isTurnEnd = false;

        manager.HideSelectCircle();
        manager.HideTargetCircle();
        manager.HideTeamTargetCircle();
    }

    private void EndGameProcessing()
    {
        if (playerList.Count == 0)
        {
            isGameEnd = true;
            manager.ShowLoseEndGamePanel();
        }
        else if (enemyList.Count == 0)
        {
            var cameraController = FindObjectOfType<CameraController>();

            if (cameraController.getIsBossFight)
            {
                isGameEnd = true;
                manager.ShowWinEndGamePanel();
                return;
            }

            Invoke("HeroMoveAnimation", 1f);
        }
    }

    private void HeroMoveAnimation()
    {
        OnKilledAllEnemies?.Invoke();
        manager.HideSelectCircle();
    }

    [ContextMenu("EndLvl")]
    public void DestroyAllCreaturesAndLoadNewScene()
    {
        foreach (var creatures in fightList)
        {
            creatures.Destroy();
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene("World");
    }

}

