using UnityEngine;

class WeaponLevel
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

    WeaponLevel sword, axe, dagger, spear, swordWithShield, bow;

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
 
}

