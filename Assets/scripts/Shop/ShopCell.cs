using UnityEngine.EventSystems;

public class ShopCell : Cell
{
    public override void Render(AssetItem item)
    {
        base.Render(item);
        _nameField.text = item.Name + " | " + item.Price;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            var inventory = FindObjectOfType<GeneralInventory>();
            var shop = FindObjectOfType<Shop>();

            if(inventory.Coins >= ItemPrice)
            { 
                inventory.Add(_item);
                //shop.Delete(_item);
                inventory.Coins -= ItemPrice;
            }
        }
    }
}
