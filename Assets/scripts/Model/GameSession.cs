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
