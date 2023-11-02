using UnityEngine;

public class GeneralInventory : Inventory
{
    public int Coins;
    public AssetItem assetItem;

    private void Start()
    {
        LoadItemsFromGameSession();
        FullRender(Items);
    }

    private void LoadItemsFromGameSession()
    {
        var session = FindObjectOfType<GameSession>();
        if(session != null) 
        {
            Items.AddRange(session.InvData.items);
        }
    }

    [ContextMenu("Add")]
    public void AddItem()
    {
        base.Add(assetItem);
        base.Add(assetItem);
        base.Add(assetItem);
        base.Add(assetItem);
        base.Add(assetItem);
        base.Add(assetItem);
        base.Add(assetItem);
    }
}
