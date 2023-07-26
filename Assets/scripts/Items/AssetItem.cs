using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Item")]
[System.Serializable]
public class AssetItem : ScriptableObject, IItem
{
    public string Name => _name;

    public Sprite UIIcon => _uiIcon;

    public string Description => _description;

    public int Price => _price;

    public int Protection => _protection;

    public int Health => _health;

    public int Evasion => _evsion;

    public ItemType itemType => _itemType;

    public GameObject prefab => _prefab;

    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private int _price;
    [SerializeField] private int _protection;
    [SerializeField] private int _health;
    [SerializeField] private int _evsion;
    [SerializeField] private Sprite _uiIcon;
    [SerializeField] private ItemType _itemType;
    [SerializeField] private GameObject _prefab;
}

public enum ItemType
{
    Helm,Armor,Weapon,Ring,Amule,Boots
}