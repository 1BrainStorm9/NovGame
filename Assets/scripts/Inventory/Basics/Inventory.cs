using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;
using static UnityEditor.Progress;

public abstract class Inventory : MonoBehaviour
{
    [SerializeField] protected List<AssetItem> Items;
    //[SerializeField] protected Cell _CellTemplate;
    [SerializeField] protected InventoryCell _inventoryCellTemplate;
    [SerializeField] protected Transform _container;
    [SerializeField] protected Transform _draggingParent;
    [SerializeField] protected List<Cell> _cells;
    
    
    [SerializeField] private GeneralInventory _secondaryInventory;
    [SerializeField] private GeneralInventory _generalInventory;

    public GeneralInventory GeneralInventoryProperty
    {
        get { return _generalInventory; }
    }

    public static int Weight = 7;
    public static int MaxWeight = 15;


    private Cell activeCell;

    public void OnEnable()
    {
        InventoryCell.OnClick += SetActiveCell;
    }

    private void OnDisable()
    {
        InventoryCell.OnClick -= SetActiveCell;
    }

    public int CurrentWeight
    {
        get { return Weight; }
        set { Weight = value; }
    }

    public int GetMaximumWeight()
    {
        return MaxWeight;
    }


    private bool CanAddItem(AssetItem assetItem)
    {
        int newWeight = Weight + assetItem.Weight;
        return newWeight <= MaxWeight;
    }

    public void Add(AssetItem item)
    {
        if (CanAddItem(item))
        {
            Items.Add(item);
            Weight += 1;
            AddItemInUi(item);
        }
        else
        {
            Debug.Log("Inventory full");
        }
    }


    public void Delete(AssetItem item)
    {

        _cells.Remove(activeCell);
        Destroy(activeCell.gameObject);
        Items.Remove(item);
        Weight -= 1;
    }


    [ContextMenu("Render")]
    protected void FullRender(List<AssetItem> items)
    {
        

        foreach (Transform child in _container)
        {
            Destroy(child.gameObject);
        }

        items.ForEach(item =>
        {
            
            AddItemInUi(item);
        });
    }

    private void SetActiveCell(Cell cell)
    {
        activeCell = cell;
    }

    private void AddItemInUi(AssetItem item)
    {
        if (item == null) return;
        var cell = Instantiate(_inventoryCellTemplate, _container);
        _cells.Add(cell);
        cell.Init(_draggingParent);
        cell.Render(item);
        cell.Id = _cells.Count + 1;
    }
}
