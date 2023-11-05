using Assets.scripts.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] private PlayerData _data;
    [SerializeField] private InventoryData _inventory;
    [SerializeField] private List<Hero> _heroes;
    [SerializeField] private TimeOfDayData _dataTime;
    [SerializeField] private QuestManager questManager;


    [SerializeField] public List<AssetQuest> quests;
    [SerializeField] public List<AssetQuest> questsIsActive;
    [SerializeField] public List<AssetQuest> questsComplete;

    public PlayerData Data => _data;
    public InventoryData InvData => _inventory;
    public List<Hero> Heroes => _heroes;
    public TimeOfDayData DataTime => _dataTime;


    private void Awake()
    {
        questManager = FindAnyObjectByType<QuestManager>();
        foreach (var quest in quests)
        {
            questManager.ActivateQuest(quest.Name);
            if (questsIsActive.Contains(quest))
            {
                questManager.CompleteQuest(quest.Name);
            }
        }

        LoadHud();
        if (isSessionExit())
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            DontDestroyOnLoad(this);
        }

    }


    public void GetTimeData()
    {
        var cycleDayNight = FindObjectOfType<CycleDayNight>();
        _dataTime.intensity =  cycleDayNight.GetLight().intensity;
    }


    private void LoadHud()
    {
        SceneManager.LoadScene("Hud", LoadSceneMode.Additive);
    }

    private bool isSessionExit()
    {
        var sessions = FindObjectsOfType<GameSession>();
        foreach (var gameSession in sessions)
        {
            if(gameSession != this)
            {
                return true;
            }
        }
        return false;
    }

}
