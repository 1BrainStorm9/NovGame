using TMPro;
using UnityEngine;


public class DisplayingItemInfo : MonoBehaviour
{
    [SerializeField] private Transform _itemInfoPanel;

    public void CreateInfoPanel(AssetItem _item)
    {

        _itemInfoPanel.GetComponentInChildren<TextMeshProUGUI>().text = $"{_item.Name}\nЗащита: {_item.Protection.ToString()}\nЗдоровье: {_item.Health.ToString()}";
    }



    public void ClearInfoPanel()
    {
        _itemInfoPanel.GetComponentInChildren<TextMeshProUGUI>().text = "Наведите на предмет для информации";
    }

}
