using System;
using UnityEngine;
using UnityEngine.EventSystems;
using static CameraController;

public class InventoryCell : Cell, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public delegate void InventoryCellDelegate(InventoryCell cell);
    public static event InventoryCellDelegate OnClick;
    public static Action<InventoryCell> OnEnterCell;

    private Transform _draggingParent;
    private Transform _originalParent;



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

    public void Init(Transform draggingParent)
    {
        _draggingParent = draggingParent;
        _originalParent = transform.parent;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.parent = _draggingParent;
        OnEnterCell?.Invoke(this);
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        int closestIndex = 0;
        var inv = FindObjectOfType<GeneralInventory>();
        var container = inv.GetContainerWithIndex();

        for (int i = 0; i < container.transform.childCount; i++)
        {
            if(Vector3.Distance(transform.position, container.GetChild(i).position) < Vector3.Distance(transform.position, container.GetChild(closestIndex).position))
            {
                closestIndex = i;
            }
        }

        if(_originalParent != container)
        {
            inv.Delete(_item);
            inv.Add(_item, inv.ItemsUp, container);
        }
        else
        {
            transform.parent = container;
        }


        transform.SetSiblingIndex(closestIndex);
    }

}
