using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonUseHandler : MonoBehaviour
{

    public void OnEquipItem()
    {
        var inv = FindObjectOfType<PlayerInventory>();
        inv.EquipItem();
    }

    public void OnDropItem()
    {
        var generalInv = FindObjectOfType<GeneralInventory>();
        var playerInv = FindObjectOfType<PlayerInventory>();
        generalInv.Delete(playerInv.item);
        FindObjectOfType<ExitMenuHandler>().DestroyMenu();
    }

}