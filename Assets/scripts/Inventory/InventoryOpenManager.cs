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
        if (Input.GetKeyDown(KeyCode.I))
        {
            string sceneName = SceneManager.GetActiveScene().name;

            
            _isVisible = !_isVisible;
            


            if (gameState == GameState.Fight)
            {
                _gereneralInventoryPanel.SetActive(_isVisible);
                _localeInventoryPanel.SetActive(_isVisible);
            }
            else
            {
                _gereneralInventoryPanel.SetActive(_isVisible);
                _localeInventoryPanel.SetActive(_isVisible);
                _cartInventory.SetActive(_isVisible);
            }

            if (_isVisible)
            {
                var plrinv = FindObjectOfType<PlayerInventory>();
                plrinv.RefreshHeroInfoPanel();
            }

        }

    }
}
