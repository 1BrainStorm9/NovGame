using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryEnter : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] int index;
    public static Action<int> OnEnter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("1");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnEnter?.Invoke(index);
    }

}
