using System;
using UnityEngine.EventSystems;
using static CameraController;

public class InventoryCell : Cell, IPointerEnterHandler, IPointerExitHandler
{
    public delegate void InventoryCellDelegate(InventoryCell cell);
    public static event InventoryCellDelegate OnClick;
    public void OnPointerEnter(PointerEventData eventData)
    {
        ShowItemStats();
    }

    private void ShowItemStats()
    {
        var infstats = FindObjectOfType<DisplayingItemInfo>();
        infstats.CreateInfoPanel(_item);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HideItemStats();
    }

    private void HideItemStats()
    {
        var infstats = FindObjectOfType<DisplayingItemInfo>();
        infstats.ClearInfoPanel();
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnClick(this);
            var plrInv = FindObjectOfType<PlayerInventory>();
            plrInv.item = _item;
            plrInv.EquipItem();
        }
    }

    public override void Render(AssetItem item)
    {
        _item = item;
        _nameField.text = item.Name;
        _iconField.sprite = item.UIIcon;
    }
}
