using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Inventory : MonoBehaviour
{
    [SerializeField] protected List<AssetItem> Items;
    [SerializeField] protected Cell _CellTemplate;
    [SerializeField] protected Transform _container;

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

    public void Delete(AssetItem item)
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
            var cell = Instantiate(_CellTemplate, _container);
            cell.Render(item);
        });
    }
}
