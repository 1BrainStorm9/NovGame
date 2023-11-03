using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryEnter : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] int index;
    public static Action<int> OnEnter;

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnEnter?.Invoke(index);
    }

}
