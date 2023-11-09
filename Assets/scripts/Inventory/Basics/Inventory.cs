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
    [SerializeField] protected Transform _draggingParent;
    [SerializeField] protected List<Cell> _cells;
    public List<AssetItem> allItems;
    public static int Weight = 7;
    public static int MaxWeight = 15;



    protected Cell activeCell;

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


    protected  bool CanAddItem(AssetItem assetItem)
    {
        int newWeight = Weight + assetItem.Weight;
        return newWeight <= MaxWeight;
    }

    public virtual void Add(AssetItem item, List<AssetItem> items,Transform container)
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


    public virtual void Delete(AssetItem item)
    {
        _cells.Remove(activeCell);
        Destroy(activeCell.gameObject);
        RemoveItems(item);
        Weight -= 1;
    }

    public void RemoveItems(AssetItem item)
    {
        var first = ItemsDown.Find(x => x == item);
        if (first != null)
        {
            allItems.Remove(first);
            ItemsDown.Remove(first);
        }
        else
        {
            var seconde = ItemsUp.Find(x => x == item);
            allItems.Remove(seconde);
            ItemsUp.Remove(seconde);
        }
    }

    [ContextMenu("Render")]
    public virtual void FullRender(List<AssetItem> items,Transform container)
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

    protected virtual void SetActiveCell(Cell cell)
    {
        activeCell = cell;
    }

   

    protected virtual void AddItemInUi(AssetItem item, Transform container)
    {
        if (item == null) return;
        var cell = Instantiate(_inventoryCellTemplate, container);
        _cells.Add(cell);
        cell.Init(_draggingParent);
        cell.Render(item);
        cell.Id = _cells.Count + 1;
    }
}
