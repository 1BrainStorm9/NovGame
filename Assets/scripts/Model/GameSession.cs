using Assets.scripts.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] private PlayerData _data;
    [SerializeField] private InventoryData _inventory;
    [SerializeField] private List<Hero> _heroes;
    [SerializeField] private TimeOfDayData _dataTime;

    
    [Serializable] public class Quest
    {
        public string questName;
        public bool isCompleted;
    }

    [SerializeField] public int questsCounter = 0;
    [SerializeField] public List<Quest> quests;

    public PlayerData Data => _data;
    public InventoryData InvData => _inventory;
    public List<Hero> Heroes => _heroes;
    public TimeOfDayData DataTime => _dataTime;

    private void Awake()
    {

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

    public void CompleteQuest(string questName)
    {
        foreach (Quest quest in quests)
        {
            if (quest.questName == questName && !quest.isCompleted)
            {
                quest.isCompleted = true;
                IncrementQuestCounter();
            }
        }
    }

    public bool IsQuestCompleted(string questName)
    {
        Quest quest = quests.Find(q => q.questName == questName);
        return quest != null && quest.isCompleted;
    }

    public void MarkCompleted(string questName)
    {
        Quest quest = quests.Find(q => q.questName == questName);
        if (quest != null)
        {
            quest.isCompleted = true;
        }
    }
    public int GetQuestCounter()
    {
        return questsCounter;
    }

    public void IncrementQuestCounter()
    {
        questsCounter++;
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
