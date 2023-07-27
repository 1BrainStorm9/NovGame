using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Inventory : MonoBehaviour
{
    [SerializeField] protected List<AssetItem> Items;
    [SerializeField] protected Cell _CellTemplate;
    [SerializeField] protected Transform _container;
    [SerializeField] protected List<Cell> _cells;
    private Cell activeCell;

    public void OnEnable()
    {
        FullRender(Items);
        InventoryCell.OnClick += SetActiveCell;
    }

    private void OnDisable()
    {
        InventoryCell.OnClick -= SetActiveCell;
    }

    public void Add(AssetItem item)
    {
        Items.Add(item);
        AddItemInUi(item);
    }

    public void Delete(AssetItem item)
    {
        _cells.Remove(activeCell);
        Destroy(activeCell.gameObject);
        Items.Remove(item);
    }

    private void FullRender(List<AssetItem> items)
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
        var cell = Instantiate(_CellTemplate, _container);
        _cells.Add(cell);
        cell.Render(item);
        cell.Id = _cells.Count + 1;
    }
}
