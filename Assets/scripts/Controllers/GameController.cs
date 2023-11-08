using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

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
    private SpawnTargetMarker spawnTargetMarker;
    private HudManager hudManager;

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
        ChoiseTarget.OnSelectTarget += SetTarget;
    }

    private void OnDisable()
    {
        EnemyManager.OnEnemyAdded -= AddEnemyToList;
        HeroManager.OnHeroAdded -= AddHeroToList;
        ChoiseTarget.OnSelectTarget -= SetTarget;
    }

    private void SetTarget(Creature target)
    {
        if (GetActiveCreature.GetComponent<CastSpellController>().isSpellSelected) 
        {
            targerObject = target;
        }
        else
        {
            targerObject = target;
            var hud = FindObjectOfType<HudManager>();
            if (hud != null) hud.UpdateEnemyStats();
        }
        
    }


    private void Start()
    {
        hudManager = FindObjectOfType<HudManager>();
        hudManager.ShowRightSide(true);
        manager = GetComponent<UIManager>();
        heroManager = GetComponent<HeroManager>();
        enemyManager = GetComponent<EnemyManager>();
        spawnTargetMarker = FindObjectOfType<SpawnTargetMarker>();

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

        if (IsPlayerTurn())
        {
            if (selectObject == null)
            {
                SelectActiveObject();
            }
        }
        else
        {
            if (!isTurnEnd)
            {
                hudManager.ShowRightSide(true);
                isTurnEnd = true;
                SelectActiveObject();
                EnemyTurnProccesing();
            }
        }
    }

    private void EnemyTurnProccesing()
    {
        targerObject = selectObject.GetComponent<Enemy>().ChooseSpellAndGetTarget();
        Invoke("EnemyTurnEnd", 1.5f);
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
            RemovePlayerWhenNoHp();
            isTurnEnd = true;
            EndTurn();
        }
    }

    private void RemoveEnemyWhenNoHp()
    {
        if (targerObject.health <= 0)
        {
            selectObject.GetComponent<Hero>().AddExp(targerObject.GetComponent<Enemy>().RewardExp * targerObject.GetComponent<Enemy>().lvl);
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
            NewTurnProccesing();
        }

    }

    private void NewTurnProccesing()
    {
        if (IsPlayerTurn())
        {
            selectObject.AddWeaponSpellsToHeroSpells();
            manager.ReloadSpellHUD();
            FindObjectOfType<HudManager>().UpdateHeroStats();
        }
        selectObject.ActivateStates();
    }

    public bool IsPlayerTurn()
    {
        if (fightList[0] == null) { return false; }
        return GetActiveCreature.GetComponent<Hero>() != null;
    }

    private void EndTurn()
    {
        EndMoveSettings();
        EndGameProcessing();
    }

    private void EndMoveSettings()
    {
        spawnTargetMarker.DeleteMarker();
        fightList.Add(GetActiveCreature);
        fightList.RemoveAt(0);

        selectObject = null;
        targerObject = null;

        isTurnEnd = false;
    }

    private void EndGameProcessing()
    {
        if (playerList.Count == 0)
        {
            Invoke("Pause", 1f);
            manager.ShowLoseEndGamePanel();
        }
        else if (enemyList.Count == 0)
        {
            var cameraController = FindObjectOfType<CameraController>();
            if (cameraController.getIsBossFight)
            {
                Invoke("Pause", 1f);
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

    [ContextMenu("End")]
    private void EndGame()
    {
        var gs = FindAnyObjectByType<GameSession>();
        List<PrefabData> list = new List<PrefabData>();

        foreach (var hero in playerList)
        {
            PrefabData prefabData = new PrefabData(hero.GetId(), hero.exp, hero.health, hero.maxHealth, hero.damage, hero.criticalDamage, hero.criticalChance, hero.protect, hero.evasionChance, hero.creatureName, hero.lvl, hero.Items, hero.weapon, hero.CharStates, hero.masteryLevel);
            //prefabData.masteryLevel.SetWeaponMastery(hero.masteryLevel.GetLevels());
            list.Add(prefabData);
        }

        gs.SavePrefabData(list);
        Invoke("Pause", 1f);
        manager.ShowWinEndGamePanel();
    }

    private void Pause()
    {
        Time.timeScale = 0f;
    }
}


[System.Serializable]
public class PrefabData
{
    public int HeroId;
    public int exp;
    public float health;
    public float maxHealth;
    public float damage;
    public float criticalDamage;
    public float criticalChance;
    public float protect;
    public float evasionChance;
    public string creatureName;
    public int lvl;
    public List<AssetItem> Items;
    public Weapon weapon;
    public List<StateInfo> CharStates;
    public WeaponMastery masteryLevel;

    

    public PrefabData(int heroId, int exp, float health, float maxHealth, float damage, float criticalDamage, float criticalChance, float protect, float evasionChance, string creatureName, int lvl, List<AssetItem> items, Weapon weapon, List<StateInfo> charStates, WeaponMastery masteryLevel)
    {
        
        HeroId = heroId;
        this.exp = exp;
        this.health = health;
        this.maxHealth = maxHealth;
        this.damage = damage;
        this.criticalDamage = criticalDamage;
        this.criticalChance = criticalChance;
        this.protect = protect;
        this.evasionChance = evasionChance;
        this.creatureName = creatureName;
        this.lvl = lvl;
        Items = items;
        this.weapon = weapon;
        CharStates = charStates;
        this.masteryLevel  = masteryLevel;
}
}
