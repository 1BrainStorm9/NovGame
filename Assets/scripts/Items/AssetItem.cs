using System;
using UnityEngine;

[CreateAssetMenu(menuName ="Item")]

[Serializable]
public class AssetItem : ScriptableObject, IItem
{
    public string Name => _name;
    public string Description => _description;

    public int Price => _price;
    public int Weight => _weight;

    public int Protection => _protection;

    public int Health => _health;

    public int Evasion => _evsion;

    public ItemType itemType => _itemType;

    public Sprite UIIcon => _uiIcon;

    public GameObject prefab => _prefab;

    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private int _protection;
    [SerializeField] private int _health;
    [SerializeField] private int _evsion;
    [SerializeField] private int _price;
    [SerializeField] private int _weight;
    [SerializeField] private Sprite _uiIcon;
    [SerializeField] private ItemType _itemType;
    [SerializeField] private GameObject _prefab;
}

public enum ItemType
{
    Helm,Armor,Weapon,Ring,Amule,Boots
}