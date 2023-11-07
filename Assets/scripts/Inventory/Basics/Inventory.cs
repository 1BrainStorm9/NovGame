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
    [SerializeField] public List<AssetItem> ItemsUp;
    [SerializeField] public List<AssetItem> ItemsDown;
    [SerializeField] protected InventoryCell _inventoryCellTemplate;
    [SerializeField] protected Transform _containerUp;
    [SerializeField] protected Transform _containerDown;
    [SerializeField] protected Transform _draggingParent;
    [SerializeField] protected List<Cell> _cells;

    public static int Weight = 7;
    public static int MaxWeight = 15;



    private Cell activeCell;

    public virtual void OnEnable()
    {
        InventoryCell.OnClick += SetActiveCell;
        InventoryCell.OnEnterCell += SetActiveCell;
    }

    public virtual void OnDisable()
    {
        InventoryCell.OnClick -= SetActiveCell;
        InventoryCell.OnEnterCell -= SetActiveCell;

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

    public void Add(AssetItem item, List<AssetItem> items,Transform container)
    {
        if (CanAddItem(item))
        {
            items.Add(item);
            Weight += 1;
            AddItemInUi(item, container);
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
        ItemsUp.Remove(item);
        Weight -= 1;
    }


    [ContextMenu("Render")]
    protected void FullRender(List<AssetItem> items,Transform container)
    {
        
        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }

        items.ForEach(item =>
        {
            
            AddItemInUi(item,container);
        });
    }

    private void SetActiveCell(Cell cell)
    {
        activeCell = cell;
    }

   

    private void AddItemInUi(AssetItem item, Transform container)
    {
        if (item == null) return;
        var cell = Instantiate(_inventoryCellTemplate, container);
        _cells.Add(cell);
        cell.Init(_draggingParent);
        cell.Render(item);
        cell.Id = _cells.Count + 1;
    }
}
