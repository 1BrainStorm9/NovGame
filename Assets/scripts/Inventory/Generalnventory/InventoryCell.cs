using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryCell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private TextMeshProUGUI _nameField;
    [SerializeField] private Image _iconField;

    private AssetItem _item;
    public InstantiateChoiseMenu searchMenu;


    private void Awake()
    {
        searchMenu = GetComponentInParent<InstantiateChoiseMenu>();
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        ShowItemStats();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HideItemStats();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            searchMenu.InstantiateMenu(eventData);
            var plrInv = FindObjectOfType<PlayerInventory>();
            plrInv.item = _item;
        }
    }

    private void ShowItemStats()
    {
        var infstats = FindObjectOfType<DisplayingItemInfo>();
        infstats.CreateInfoPanel(_item);
    }

    private void HideItemStats()
    {
        var infstats = FindObjectOfType<DisplayingItemInfo>();
        infstats.ClearInfoPanel();
    }

    public void Render(AssetItem item)
    {
        _item = item;
        _nameField.text = item.Name;
        _iconField.sprite = item.UIIcon;
    }


}
