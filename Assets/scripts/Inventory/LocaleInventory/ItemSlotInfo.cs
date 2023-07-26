using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlotInfo : MonoBehaviour, IPointerClickHandler
{
    public bool isEmptySlot;
    public AssetItem item;
    public Sprite defaultImage;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isEmptySlot && eventData.button == PointerEventData.InputButton.Right)
        {
            var plrInv = FindObjectOfType<PlayerInventory>();
            plrInv.WithdrawItem(item);
        }
    }

}
