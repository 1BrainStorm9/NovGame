using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shop : Inventory
{
    [SerializeField] protected ShopCell shopCell;
    [SerializeField] public int index;
    [SerializeField] public Transform InvUp;
    [SerializeField] public Transform InvDown;
    [SerializeField] public int moneyCount;
    [SerializeField] public GeneralInventory generalInventory;
    [SerializeField] private TextMeshProUGUI torgText;
    [SerializeField] private TextMeshProUGUI urText;

    private void Awake()
    {
        AddHeroItems();
        FullRender(ItemsUp, InvUp);
        FullRender(ItemsDown, InvDown);
        UpdateMoney();

    }

    public void AddHeroItems()
    {
        ItemsDown.AddRange(generalInventory.ItemsUp);
        ItemsDown.AddRange(generalInventory.ItemsDown);
    }

    public void UpdateMoney()
    {
        torgText.text = "Tорговец: " + moneyCount;
        urText.text = "Вы: " + generalInventory.Coins;
    }

    public override void OnEnable()
    {
        ShopCell.OnClick += SetActiveCell;
        InventoryEnter.OnEnter += changeIndexInv;
    }

    public override void OnDisable()
    {
        ShopCell.OnClick -= SetActiveCell;
        InventoryEnter.OnEnter -= changeIndexInv;
    }


    private void changeIndexInv(int index)
    {
        this.index = index;
    }

    public override void FullRender(List<AssetItem> items, Transform container)
    {

        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }

        items.ForEach(item =>
        {

            this.AddItemInUi(item, container);
        });
    }

    protected override void AddItemInUi(AssetItem item, Transform container)
    {
        if (item == null) return;
        var cell = Instantiate(shopCell, container);
        _cells.Add(cell);
        cell.Render(item);
        cell.Id = _cells.Count + 1;
    }

    public override void Delete(AssetItem item)
    {
        
        var inv = GetInvWithIndex();
        inv.Remove(item);
        _cells.Remove(activeCell);
        Destroy(activeCell.gameObject);
    }

    public override void Add(AssetItem item, List<AssetItem> items, Transform container)
    {
        if (CanAddItem(item))
        {
            items.Add(item);
           // Weight += 1;
            this.AddItemInUi(item, container);
        }
        else
        {
            Debug.Log("Inventory full");
        }
    }

    public Transform GetContainerWithIndex()
    {
        switch (index)
        {
            case 0:
                return InvUp;
            case 1:
                return InvDown;
            default: return InvUp;
        }
    }

    public List<AssetItem> GetInvWithIndex()
    {
        switch (index)
        {
            case 0:
                return ItemsUp;
            case 1:
                return ItemsDown;
            default: return ItemsUp;
        }
    }


}
