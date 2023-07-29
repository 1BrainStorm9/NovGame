using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChoiseHeroIndexInPlayerInv : MonoBehaviour
{
    public void OnButtonClick()
    {
        Button button = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        ButtonInfo buttonInfo = button.GetComponent<ButtonInfo>();
        int index = buttonInfo.index;
        var localeInventory = FindObjectOfType<PlayerInventory>();
        localeInventory.SetIndexAndRefresh(index);
    }


}
