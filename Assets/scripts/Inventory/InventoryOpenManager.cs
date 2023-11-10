using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using static UnityEditor.Progress;

public class InventoryOpenManager : MonoBehaviour
{
    [SerializeField] private GameObject _gereneralInventoryPanel;
    [SerializeField] private GameObject _localeInventoryPanel;
    [SerializeField] private GameObject _cartInventory;
    [SerializeField] private GameObject _hudCanvas;
    [SerializeField] private GameObject _shopCanvas;
    [SerializeField] private bool _isVisible = false;
    [SerializeField] private bool isPeace;


    private GameState gameState = GameState.Fight;

    public enum GameState
    {
        Fight, Peace
    }
    private void Start()
    {
        /*if (isPeace)
            {
                gameState = GameState.Peace;
                _hudCanvas.SetActive(false);
            }
            else
            {
                gameState = GameState.Fight;
                _hudCanvas.SetActive(true);
            }*/
    }

    private void Update()
    {
        

    }
}
