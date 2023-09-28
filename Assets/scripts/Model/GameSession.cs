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
    public PlayerData Data => _data;
    public InventoryData InvData => _inventory;
    public List<Hero> Heroes => _heroes;

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
