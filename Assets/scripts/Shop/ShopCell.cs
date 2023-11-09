using System;
using System.Data.Common;
using UnityEngine.EventSystems;

public class ShopCell : Cell
{
    public delegate void InventoryCellDelegate(ShopCell cell);
    public static event InventoryCellDelegate OnClick;

    public override void Render(AssetItem item)
    {
        base.Render(item);
        _nameField.text = item.Name + " | " + item.Price;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnClick(this);
            var shop = FindObjectOfType<Shop>();
            ChoiseInv(shop.index);
        }
    }

    public void ChoiseInv(int index)
    {
        var shop = FindObjectOfType<Shop>();
        switch (index)
        {
            case 0:
                if (shop.generalInventory.Coins >= ItemPrice)
                {

                    shop.Add(_item, shop.ItemsDown, shop.InvDown);
                    shop.Delete(_item);
                    shop.generalInventory.Add(_item, shop.generalInventory.ItemsDown, shop.generalInventory.InvDown);
                    shop.generalInventory.allItems.Add(_item);

                    shop.generalInventory.Coins -= ItemPrice;
                    shop.moneyCount += ItemPrice;
                    shop.UpdateMoney();
                }
                break;
            case 1:
                if (shop.moneyCount >= ItemPrice)
                {

                    shop.Delete(_item);
                    shop.Add(_item, shop.ItemsUp, shop.InvUp);
                    shop.generalInventory.RemoveItems(_item);
                    shop.generalInventory.FullRender(shop.generalInventory.ItemsUp, shop.generalInventory.InvUp);
                    shop.generalInventory.FullRender(shop.generalInventory.ItemsDown, shop.generalInventory.InvDown);
                    shop.moneyCount -= ItemPrice;
                    shop.generalInventory.Coins += ItemPrice;
                    shop.UpdateMoney();
                }
                break;
        }
    }
}
