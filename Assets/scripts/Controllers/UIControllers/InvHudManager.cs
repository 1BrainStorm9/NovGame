using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvHudManager : MonoBehaviour
{
    [SerializeField] private Image weaponSlot;
    [SerializeField] private Image helmSlot;
    [SerializeField] private Image armorSlot;
    [SerializeField] private Image bootsSlot;

    [SerializeField] private Sprite weaponDefault;
    [SerializeField] private Sprite helmDefault;
    [SerializeField] private Sprite armorDefault;
    [SerializeField] private Sprite bootsDefault;
    private GameController gc;

    private void Awake()
    {
        gc = FindObjectOfType<GameController>();
    }

    public void UpdateInvImages()
    {
        var creature = gc.GetActiveCreature;
        SetSprite(weaponSlot,ItemType.Weapon);
        SetSprite(helmSlot, ItemType.Helm);
        SetSprite(armorSlot, ItemType.Armor);
        SetSprite(bootsSlot, ItemType.Boots);
    }

    private void SetSprite(Image image, ItemType type)
    {
        var creature = gc.GetActiveCreature;
        if (creature.ReturnItemWhithThisType(type) == null)
        {
            SetDefaultImage(type);
            return;
        }
        image.sprite = creature.ReturnItemWhithThisType(type).UIIcon;
    }

    private void SetDefaultImage(ItemType type)
    {
        switch (type)
        {
            case ItemType.Weapon:
                weaponSlot.sprite = weaponDefault;
                break;
            case ItemType.Helm:
                helmSlot.sprite = helmDefault;
                break;
            case ItemType.Armor:
                armorSlot.sprite = armorDefault;
                break;
            case ItemType.Boots:
                bootsSlot.sprite = bootsDefault;
                break;
        }
    }

}
