using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class GameController : MonoBehaviour
{
    [Header("HUD")]
    public UIManager manager;


    [Header("Game Objects")]
    public Entity selectObject;
    public Entity targerObject;

    [Header("Lists")]
    public List<Entity> fightList;
    public List<Player> playerList;
    public List<Enemy> enemyList;

    private bool isTurnEnd = false;
    public bool isBossFight = false;

    public int heroSpeed = 0;

    private ColliderCreationController enemyColiderController;

    public delegate void NoActiveEnemiesEnterEvent();
    public static event NoActiveEnemiesEnterEvent OnKillAllEnemies;

    public Entity GetActiveCreature { get { return fightList[0]; } }

    private void OnEnable()
    {
        GetEnemiesInCollider.OnEnemyTriggerEnter += AddEnemyToList;
        CameraController.OnTriggerEnter += setBasicHeroSpeed;
    }

    private void OnDisable()
    {
        GetEnemiesInCollider.OnEnemyTriggerEnter -= AddEnemyToList;
        CameraController.OnTriggerEnter -= setBasicHeroSpeed;
    }

    private void setBasicHeroSpeed()
    {
        heroSpeed = 0;
    }

    private void Start()
    {
        enemyColiderController = FindObjectOfType<ColliderCreationController>();
        enemyColiderController.AddCollider(Component.FindAnyObjectByType<Camera>().transform);
        manager = GetComponent<UIManager>();

        foreach (var player in GameObject.FindGameObjectsWithTag("Player").ToList())
        {
            playerList.Add(player.GetComponent<Player>());
            fightList.Add(player.GetComponent<Player>());
        }
    }

    private void AddEnemyToList(Enemy enemy)
    {
        enemyList.Add(enemy);
        fightList.Add(enemy);
        ReloadListsIndex();
    }

    private void ReloadListsIndex()
    {
        ReloadFightListIndexes();
        ReloadEntityListIndexes();
    }

    private void FixedUpdate()
    {
        foreach (var item in playerList)
        {
            var moveAmount = heroSpeed * Time.deltaTime;
            item.transform.position += Vector3.right * moveAmount;
        }
    }

    private void Update()
    {
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
        targerObject = selectObject.GetComponent<Enemy>().AIChooseTarget(playerList);

        manager.ShowTargetCircle();
        manager.SetTargetCirclePosition(targerObject.transform.position);

        Invoke("EnemyTurnEnd", 1.5f);
    }

    private void EnemyTurnEnd()
    {
        selectObject.GetComponent<Enemy>().EnemyTurn();

        DestroyPlayerWhenNoHp();
        EndTurn();
    }

    private void DestroyPlayerWhenNoHp()
    {
        if (targerObject.health <= 0)
        {
            manager.HideTargetCircle();
            targerObject.Destroy();
            fightList.RemoveAt(targerObject.fightIndex);
            playerList.RemoveAt(targerObject.listIndex);
        }
    }

    public void UseCreatureSpell()
    {
        if (selectObject != null && targerObject != null && !isTurnEnd)
        {
            selectObject.GetComponent<CastSpellController>().UseSpell(targerObject);
            DestroyEnemyWhenNoHp();
            isTurnEnd = true;
            EndTurn();
        }
    }

    private void DestroyEnemyWhenNoHp()
    {
        if (targerObject.health <= 0)
        {
            manager.HideTargetCircle();
            targerObject.Destroy();
            fightList.RemoveAt(targerObject.fightIndex);
            enemyList.RemoveAt(targerObject.listIndex);
        }
    }

    private void SelectActiveObject()
    {
        selectObject = GetActiveCreature;
        enemyColiderController.DeleteCollider();
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
        enemyColiderController.DeleteCollider();
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        var csc = selectObject.GetComponent<CastSpellController>();
       

        if (csc.SpellSelected && csc.CanCastForEnemy() && hit.collider?.tag == "Enemy")
        {
            SelectTargetProccesing(hit, csc.CanCastForEnemy());
        }
        else if(csc.SpellSelected && !csc.CanCastForEnemy() && hit.collider?.tag == "Player")
        {
            SelectTargetProccesing(hit, csc.CanCastForEnemy());
        }


    }

    private void SelectTargetProccesing(RaycastHit2D hit, bool castForEnemy)
    {
        targerObject = hit.collider.GetComponent<Entity>();
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
        return GetActiveCreature.GetComponent<Player>() != null;
    }

    private void EndTurn()
    {
        EndMoveSettings();
        ReloadListsIndex();
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
            DestroyAllCreaturesAndLoadNewScene();
        }
        else if (enemyList.Count == 0)
        {
            var cameraController = FindObjectOfType<CameraController>();

            if (cameraController.getIsBossFight)
            {
                DestroyAllCreaturesAndLoadNewScene();
            }

            OnKillAllEnemies?.Invoke();

            heroSpeed = 5;
            manager.HideSelectCircle();
        }
    }

    private void DestroyAllCreaturesAndLoadNewScene()
    {
        foreach (var creatures in fightList)
        {
            creatures.Destroy();
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene("NewScene");
    }


    private void ReloadFightListIndexes()
    {
        for (var i = 0; i < fightList.Count; i++)
        {
            fightList[i].fightIndex = i;
        }
    }

    private void ReloadEntityListIndexes()
    {
        for (var i = 0; i < playerList.Count; i++)
        {
            playerList[i].listIndex = i;
        }

        for (var i = 0; i < enemyList.Count; i++)
        {
            enemyList[i].listIndex = i;
        }
    }

}
