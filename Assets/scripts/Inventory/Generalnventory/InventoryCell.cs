using System;
using UnityEngine;
using UnityEngine.EventSystems;


public class InventoryCell : Cell, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public delegate void InventoryCellDelegate(InventoryCell cell);
    public static event InventoryCellDelegate OnClick;
    public static Action<InventoryCell> OnEnterCell;

    private Transform _draggingParent;
    private Transform _originalParent;
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    public bool dragOnSurfaces = true;
    [SerializeField] private GameObject _panel;
    private GameObject cell;



    public override void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if ((FindObjectOfType<SceneType>().GetSceneType() == SceneEnum.peaceScene || FindObjectOfType<GameController>().IsPlayerTurn()))
            {
                OnClick(this);
                AddItemToHero(); 
            }

        }
    }

    private void AddItemToHero()
    {
        var hero = FindObjectOfType<GameController>().GetActiveCreature;

        if (hero == null || _item == null)
        {
            return;
        }

        var item = FindItem(hero);

        if (item != null)
        {
            hero.Items.Remove(item);
            hero.Items.Add(_item);
            if (item.itemType == ItemType.Weapon)
            {
                DeleteWeapon(hero);
                AddWeapon(hero);
            }
            var inv = FindObjectOfType<GeneralInventory>();
            var container = inv.GetContainerWithIndex();
            inv.Add(item, inv.GetInvWithIndex(), container);
            inv.Delete(_item);
            var hud = FindObjectOfType<HudManager>();
            hud.UpdateHeroStats();
            return;
        }
        else
        {
            hero.Items.Add(_item);
            if (_item.itemType == ItemType.Weapon)
            {
                AddWeapon(hero);
            }
            var inv = FindObjectOfType<GeneralInventory>();
            inv.Delete(_item);
            FindObjectOfType<HudManager>().UpdateHeroStats();
            return;
        }

    }

    private void AddWeapon(Creature hero)
    {
        var weapon = Instantiate(_item.prefab);
        weapon.transform.SetParent(hero.transform);
        hero.weapon = weapon.GetComponent<Weapon>();
        hero.AddWeaponSpellsToHeroSpells();
        hero.AddWeaponDamage();
        FindObjectOfType<SpellHUDController>().ReloadHUD();
    }

    private static void DeleteWeapon(Creature hero)
    {
        hero.DeleteWeaponDamage();
        hero.weapon = null;
        Destroy(hero.GetComponentInChildren<Weapon>().gameObject);
    }

    private AssetItem FindItem(Creature hero)
    {
        foreach (var item in hero.Items)
        {
            if(item.itemType == _item.itemType)
            {
                return item;
            }
        }
        return null;
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
