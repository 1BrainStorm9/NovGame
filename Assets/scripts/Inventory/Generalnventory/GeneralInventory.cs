using Ink.Parsed;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GeneralInventory : Inventory
{
    public int Coins;
    public AssetItem assetItem;
    [SerializeField] private int index;
    [SerializeField] public Transform InvUp;
    [SerializeField] public Transform InvDown;


    private void Awake()
    {
       
        LoadItemsFromGameSession();
        allItems.AddRange(ItemsDown);
        allItems.AddRange(ItemsUp);
        FullRender(ItemsUp, InvUp);
        FullRender(ItemsDown, InvDown);
    }


    public override void OnEnable()
    {
        base.OnEnable();
        InventoryEnter.OnEnter += changeIndexInv;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        InventoryEnter.OnEnter -= changeIndexInv;
    }

    private void changeIndexInv(int index)
    {
        this.index = index;
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

    private void LoadItemsFromGameSession()
    {
        var session = FindObjectOfType<GameSession>();
        if(session != null) 
        {
            ItemsUp.AddRange(session.InvData.items);
        }
    }

}
