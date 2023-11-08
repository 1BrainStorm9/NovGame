using Ink.Parsed;
using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponLevel
{
    public WeaponType type;
    public int lvl;

    public WeaponLevel()
    {
        lvl = 1;
    }
}

public class WeaponMastery : MonoBehaviour
{

    public WeaponLevel sword, axe, dagger, spear, swordWithShield, bow;

    public WeaponMastery()
    {
        sword = new WeaponLevel();
        axe = new WeaponLevel();
        dagger = new WeaponLevel();
        spear = new WeaponLevel();
        swordWithShield = new WeaponLevel();
        bow = new WeaponLevel();
    }


    public void WeaponMasteryUp(WeaponType type)
    {
        switch (type)
        {
            case WeaponType.Sword:
                sword.lvl++;
                break;
            case WeaponType.Dagger:
                dagger.lvl++;
                break;
            case WeaponType.Spear:
                spear.lvl++;
                break;
            case WeaponType.SwordWithShield:
                swordWithShield.lvl++;
                break;
            case WeaponType.Bow:
                bow.lvl++;
                break;
            case WeaponType.Axe:
                axe.lvl++;
                break;
            default: 
                break;
        }
    }

    public int GetWeaponLevel(WeaponType type)
    {     
        switch (type)
        {
            case WeaponType.Sword:
                return sword.lvl;
            case WeaponType.Dagger:
                return dagger.lvl;
            case WeaponType.Spear:
                return spear.lvl;
            case WeaponType.SwordWithShield:
                return swordWithShield.lvl;
            case WeaponType.Bow:
                return bow.lvl;
            case WeaponType.Axe:
                return axe.lvl;
            default:
                return 1;
        }
    
    }

    public List<WeaponLevel> GetLevels()
    {
        List<WeaponLevel> list = new List<WeaponLevel>
        {
            sword,
            bow,
            axe,
            spear,
            dagger,
            swordWithShield
        };
        return list;
    }

    public void SetWeaponMastery(List<WeaponLevel> list)
    {
        foreach (WeaponLevel item in list)
        {

            switch (item.type)
            {
                case WeaponType.Sword:
                    sword = item;
                    break;
                case WeaponType.Dagger:
                    dagger = item;
                    break;
                case WeaponType.Spear:
                    spear = item;
                    break;
                case WeaponType.SwordWithShield:
                    swordWithShield = item;
                    break;
                case WeaponType.Bow:
                    bow = item;
                    break;
                case WeaponType.Axe:
                    axe = item;
                    break;
                default:
                    break;
            }
        }
    }

}

