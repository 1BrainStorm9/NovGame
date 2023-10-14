using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] public AssetItem asset;
    [SerializeField] public List<Spell> spells;
    [SerializeField] public float damage;
    [SerializeField] public WeaponType weaponType;


}

public enum WeaponType
{
    Sword,Axe,Dagger,Spear,SwordWithShield,Bow
}