
public class GeneralInventory : Inventory
{
    public int Coins;

    private void Start()
    {
        LoadItemsFromGameSession();
        FullRender(Items);
    }

    private void LoadItemsFromGameSession()
    {
        var session = FindObjectOfType<GameSession>();
        Items.AddRange(session.InvData.items);
    }

    
}
