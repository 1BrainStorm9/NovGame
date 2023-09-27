using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class Cell : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] protected TextMeshProUGUI _nameField;
    [SerializeField] protected Image _iconField;
    protected AssetItem _item;
    public int Id;


    public int ItemPrice=> _item.Price;
    public AssetItem Item => _item;

    public virtual void OnPointerClick(PointerEventData eventData)
    {
    }

    public virtual void Render(AssetItem item)
    {
        _item = item;
        _nameField.text = item.Name;
        _iconField.sprite = item.UIIcon;
    }
}
