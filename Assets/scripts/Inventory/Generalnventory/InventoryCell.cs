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
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    public bool dragOnSurfaces = true;

    private GameObject m_DraggingIcon;
    private RectTransform m_DraggingPlane;


    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }



    private void ShowItemStats()
    {
        var infstats = FindObjectOfType<DisplayingItemInfo>();
        infstats.CreateInfoPanel(_item);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
       
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
        canvasGroup = GetComponentInParent<CanvasGroup>();
        rectTransform = GetComponentInParent<RectTransform>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(_draggingParent, false);
        OnEnterCell?.Invoke(this);
        var canvas = GetComponentInParent<Canvas>();
        if (dragOnSurfaces)
            m_DraggingPlane = transform as RectTransform;
        else
            m_DraggingPlane = canvas.transform as RectTransform;
    }

    public void OnDrag(PointerEventData eventData)
    {
        var rt = this.GetComponent<RectTransform>();
        rt.anchoredPosition = eventData.position;

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
                        
            inv.Add(_item, inv.GetInvWithIndex(), container);
        }
        else
        {
         transform.parent = container;
        }
        transform.SetSiblingIndex(closestIndex);
    }
}
