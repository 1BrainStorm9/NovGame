using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralInventory : MonoBehaviour
{
    [SerializeField] private List<AssetItem> Items;
    [SerializeField] private InventoryCell _inventoryCellTemplate;
    [SerializeField] private Transform _container;

    public void OnEnable()
    {
        Render(Items);
    }

    public void Add(AssetItem item)
    {
        Items.Add(item);
    }

    public void AddAll(List<AssetItem> items)
    {
        Items.AddRange(items);
    }

    public void Delete (AssetItem item)
    {
        Items.Remove(item);
        Render(Items);
    }

    public void QuickRender()
    {
        Render(Items);
    }

    public void Render(List<AssetItem> items)
    {
        foreach (Transform child in _container)
        {
            Destroy(child.gameObject);
        }

        items.ForEach(item =>
        {
            var cell = Instantiate(_inventoryCellTemplate, _container);
            cell.Render(item);
        });
    }
}
