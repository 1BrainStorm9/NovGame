using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    [Header("Item Slots")]
    [SerializeField] private GameObject _helmSlot;
    [SerializeField] private GameObject _armorSlot;
    [SerializeField] private GameObject _weaponSlot;
    [SerializeField] private GameObject _ringSlot;
    [SerializeField] private GameObject _amuleSlot;
    [SerializeField] private GameObject _bootsSlot;

    private GameObject _activeSlot;

    [Header("Items info")]
    public AssetItem item;
    [SerializeField] private List<AssetItem> _activeItems;

    [Header("Heroes info")]
    public List<Creature> heroes;
    public int index = 0;

    [Header("Other info")]
    private GameController gameController;
    private DisplayingHeroInfo displayingHeroInfo;

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();

        if (gameController == null) return;

        displayingHeroInfo = FindObjectOfType<DisplayingHeroInfo>();;
        SetAllHeroes();
        Refresh();
    }

    private void SetAllHeroes()
    {
        heroes.AddRange(gameController.playerList);
    }

    private void Refresh()
    {
        RefreshSlotsToDefault();
        AddAllItem(heroes[index].Items);
        if (heroes[index].weapon != null)
        {
            AddItem(heroes[index].weapon.asset);
        }
        ChangeActiveSlotSprite();
        RefreshHeroInfoPanel();
    }

    private void RefreshSlotsToDefault()
    {
        _helmSlot.transform.GetChild(0).GetComponent<Image>().sprite = _helmSlot.GetComponent<ItemSlotInfo>().defaultImage;
        _bootsSlot.transform.GetChild(0).GetComponent<Image>().sprite = _bootsSlot.GetComponent<ItemSlotInfo>().defaultImage;
        _armorSlot.transform.GetChild(0).GetComponent<Image>().sprite = _armorSlot.GetComponent<ItemSlotInfo>().defaultImage;
        _weaponSlot.transform.GetChild(0).GetComponent<Image>().sprite = _weaponSlot.GetComponent<ItemSlotInfo>().defaultImage;
        _amuleSlot.transform.GetChild(0).GetComponent<Image>().sprite = _amuleSlot.GetComponent<ItemSlotInfo>().defaultImage;
        _ringSlot.transform.GetChild(0).GetComponent<Image>().sprite = _ringSlot.GetComponent<ItemSlotInfo>().defaultImage;

        _ringSlot.GetComponent<ItemSlotInfo>().isEmptySlot = true;
        _helmSlot.GetComponent<ItemSlotInfo>().isEmptySlot = true;
        _bootsSlot.GetComponent<ItemSlotInfo>().isEmptySlot = true;
        _armorSlot.GetComponent<ItemSlotInfo>().isEmptySlot = true;
        _weaponSlot.GetComponent<ItemSlotInfo>().isEmptySlot = true;
        _amuleSlot.GetComponent<ItemSlotInfo>().isEmptySlot = true;

        _activeItems.Clear();
    }

    public void AddItem(AssetItem item)
    {
        _activeItems.Add(item);
    }

    public void AddAllItem(List<AssetItem> items)
    {
        _activeItems.AddRange(items);
    }

    private void ChangeActiveSlotSprite()
    {
        foreach (var item in _activeItems)
        {
            SetActiveSloaAsItemType(item);
            var img = _activeSlot.transform.GetChild(0).GetComponent<Image>();
            img.sprite = item.UIIcon;
            _activeSlot.GetComponent<ItemSlotInfo>().isEmptySlot = false;
            _activeSlot.GetComponent<ItemSlotInfo>().item = item;
        }
    }

    public void SetActiveSloaAsItemType(AssetItem item)
    {
        switch (item.itemType)
        {
            case ItemType.Helm:
                _activeSlot = _helmSlot;
                break;
            case ItemType.Armor:
                _activeSlot = _armorSlot;
                break;
            case ItemType.Weapon:
                _activeSlot = _weaponSlot;
                WeaponInstantinate(item);
                break;
            case ItemType.Ring:
                _activeSlot = _ringSlot;
                break;
            case ItemType.Amule:
                _activeSlot = _amuleSlot;
                break;
            case ItemType.Boots:
                _activeSlot = _bootsSlot;
                break;
            default:
                break;
        }
    }

    private void WeaponInstantinate(AssetItem item)
    {
        if (heroes[index].weapon == null)
        {
            var weapon = Instantiate(item.prefab);
            weapon.transform.SetParent(heroes[index].transform);
            heroes[index].weapon = weapon.GetComponent<Weapon>();
            heroes[index].AddWeaponSpellsToHeroSpells();
            heroes[index].AddWeaponDamage();
            FindObjectOfType<UIManager>().ReloadSpellHUD();
        }
    }

    public void EquipItem()
    {
        if (item != null)
        {
            SetActiveSloaAsItemType(item);

            var slotIsEmpty = _activeSlot.GetComponent<ItemSlotInfo>().isEmptySlot;
            var oldItem = heroes[index].ReturnItemWhithThisType(item.itemType);

            if (!slotIsEmpty)
            {
                
                SwapItems(oldItem, item);
            }
            else
            {
                SetNewItem(item);
            }

            _activeSlot.transform.GetChild(0).GetComponent<Image>().sprite = item.UIIcon;
            RefreshHeroInfoPanel();
        }
    }

    private void SetNewItem(AssetItem newItem)
    {
        heroes[index].Items.Add(newItem);
        var generalInventory = FindObjectOfType<GeneralInventory>();
        generalInventory.Delete(newItem);

        _activeSlot.GetComponent<ItemSlotInfo>().isEmptySlot = false;
        _activeSlot.GetComponent<ItemSlotInfo>().item = newItem;

        IncHeroStats(newItem);
    }

    private void SwapItems(AssetItem oldItem, AssetItem newItem)
    {
        var generalInventory = FindObjectOfType<GeneralInventory>();

        if (newItem.itemType == ItemType.Weapon)
        {
            heroes[index].DeleteWeaponDamage();
            heroes[index].weapon = null;
            DestroyWeapon();
            WeaponInstantinate(newItem);
        }

        generalInventory.Delete(newItem);
        //generalInventory.Add(oldItem);

        heroes[index].Items.Remove(oldItem);
        heroes[index].Items.Add(newItem);

        _activeSlot.GetComponent<ItemSlotInfo>().item = newItem;
        DecHeroStats(oldItem);
        IncHeroStats(newItem);
    }

    public void WithdrawItem(AssetItem item)
    {
        var generalInventory = FindObjectOfType<GeneralInventory>();

        if (Inventory.Weight < Inventory.MaxWeight)
        {
            //generalInventory.Add(item);

            if (item.itemType == ItemType.Weapon)
            {
                DropWeapon(item);
            }
            else
            {
                heroes[index].Items.Remove(item);
            }

            SetActiveSloaAsItemType(item);
            _activeSlot.transform.GetChild(0).GetComponent<Image>().sprite = _activeSlot.GetComponent<ItemSlotInfo>().defaultImage;
            _activeSlot.GetComponent<ItemSlotInfo>().isEmptySlot = true;
        }
        else
        {
            Debug.Log("inventory full");
        }
    }



    private void DropWeapon(AssetItem item)
    {
        heroes[index].DeleteWeaponDamage();
        heroes[index].Items.Remove(item);
        DestroyWeapon();
        heroes[index].RefreshSpellsToBasic();
        FindObjectOfType<UIManager>().ReloadSpellHUD();
    }



    private void DestroyWeapon()
    {
        Destroy(heroes[index].GetComponentInChildren<Weapon>().gameObject);
    }

    private void IncHeroStats(AssetItem assetItem)
    {
        heroes[index].health += assetItem.Health;
        heroes[index].protect += assetItem.Protection;
        heroes[index].evasionChance += assetItem.Evasion;
    }

    private void DecHeroStats(AssetItem assetItem)
    {
        heroes[index].health -= assetItem.Health;
        heroes[index].protect -= assetItem.Protection;
        heroes[index].evasionChance -= assetItem.Evasion;
    }

    public void RefreshHeroInfoPanel()
    {
        displayingHeroInfo.RefreshInfoPanel(heroes[index]);
    }

    public void SetIndexAndRefresh(int tempIndex)
    {
        index = tempIndex;
        Refresh();
    }
}
