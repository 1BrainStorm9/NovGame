using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class HudManager : MonoBehaviour
{
    [SerializeField] GameObject health;
    [SerializeField] GameObject heroStats;
    [SerializeField] GameObject heroName;
    [SerializeField] GameObject enemyTargetName;
    [SerializeField] GameObject enemyTargetStats;
    [SerializeField] GameObject enemyTargetHealth;
    [SerializeField] GameObject inventoryHud;
    [SerializeField] GameObject spellHud;
    [SerializeField] GameObject enemySide;
    [SerializeField] GameObject textIn;
    [SerializeField] GameObject invDown;

    private SceneEnum sceneType;
    private bool isOpen;
    private GameController gameController;

    void Start()
    {
        var type = FindObjectOfType<SceneType>();
        if (type != null) sceneType = type.GetSceneType();
        HideHud();

        gameController = FindObjectOfType<GameController>();
        UpdateHeroStats();
    }

    private void Update()
    {
        OpenInv();
    }

    private void HideHud()
    {
        switch (sceneType)
        {
            case SceneEnum.fightScene:
                spellHud.SetActive(true);
                inventoryHud.SetActive(false);
                ShowText(true);
                invDown.SetActive(false);
                isOpen = false;
                break;
                                           
            case SceneEnum.peaceScene:
                spellHud.SetActive(false);
                inventoryHud.SetActive(false);
                ShowText(false);
                invDown.SetActive(true);
                isOpen = false;
                break;
            
        }
    }

    private void OpenInv()
    {
        if(Input.GetKeyUp(KeyCode.I)) {
            isOpen = !isOpen;
            inventoryHud.SetActive(isOpen);
        }
    }

    public void UpdateHeroStats()
    {
        if (gameController == null) return;

        var creature = gameController.GetActiveCreature;
        if (creature == null) return;
        var info = creature.GetEntityInfo();
        GetComponentInParent<InvHudManager>().UpdateInvImages();
        heroName.GetComponent<TextMeshProUGUI>().text = creature.creatureName;
        health.GetComponent<TextMeshProUGUI>().text = info["Health"] + "/" + info["HealthMax"];
        heroStats.GetComponent<TextMeshProUGUI>().text = info["Damage"] + "\n" + info["Protect"] + "\n" + info["Evasion"] + "\n" + info["CritChance"] + "\n" + info["CritMult"];

    }

    public void UpdateEnemyStats()
    {
        if (gameController == null) return;

        var creature = gameController.targerObject;
        if(creature == null)
        {
            enemySide.SetActive(true);
            return;
        }
        var info = creature.GetEntityInfo();
        enemyTargetHealth.GetComponent<TextMeshProUGUI>().text = info["Health"] + "/" + info["HealthMax"];
        enemyTargetStats.GetComponent<TextMeshProUGUI>().text = info["Damage"] + "\n" + info["Protect"] + "\n" + info["Evasion"] + "\n" + info["CritChance"];
        enemyTargetName.GetComponent<TextMeshProUGUI>().text = creature.creatureName;
    }


    public void ShowRightSide(bool isHaveTarget)
    {
        enemySide.SetActive(isHaveTarget);
    }

    public void ShowText(bool isFight)
    {
        textIn.SetActive(isFight);
    }
}


//            { "Damage", damage},
//            { "Health", health},
//            { "HealthMax", maxHealth},
//            { "Evasion", evasionChance},
//            { "Protect", protect},
//            { "CritChance", criticalChance},
//            { "CritMult", criticalDamage},
//            { "CharStates", CharStates},