using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.Progress;

public class InventoryOpenManager : MonoBehaviour
{
    [SerializeField] private GameObject _gereneralInventoryPanel;
    [SerializeField] private GameObject _localeInventoryPanel;
    [SerializeField] private GameObject _itemInfoPanel;
    [SerializeField] private GameObject _heroInfoPanel;
    [SerializeField] private bool _isVisible = false;



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            _isVisible = !_isVisible;
            _gereneralInventoryPanel.SetActive(_isVisible);
            _itemInfoPanel.SetActive(_isVisible);
            _localeInventoryPanel.SetActive(_isVisible);
            _heroInfoPanel.SetActive(_isVisible);

            if (_isVisible)
            {
               var plrinv =  FindObjectOfType<PlayerInventory>();
               plrinv.RefreshHeroInfoPanel();
            }
        }

    }
}
