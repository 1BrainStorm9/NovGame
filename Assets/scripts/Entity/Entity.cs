using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public abstract class Entity : MonoBehaviour
{
    [Header("Characteristics")]
    public float damage;
    public float criticalDamage;
    public float criticalChance;
    public float health;
    public float protect;
    public float evasionChance;

    [Header("System info")]
    public bool SpellsInHUD = false;
    public int fightIndex, listIndex;
    public List<StateInfo> CharStates;
    public List<AssetItem> Items;
    public Weapon weapon;

    public List<Spell> uniqueBasicSpells;

    public void AddWeaponSpellsToHeroSpells()
    {
        if (weapon != null)
        {
            var castSpellController = GetComponentInParent<CastSpellController>();
            RefreshSpellsToBasic();
            castSpellController.SetActiveSpells(weapon.spells);
        }
    }

    public void RefreshSpellsToBasic()
    {
        var castSpellController = GetComponentInParent<CastSpellController>();
        castSpellController.Spells.Clear();
        castSpellController.Spells.AddRange(uniqueBasicSpells);
    }

    public void ActivateStates()
    {
            foreach (var states in CharStates.ToList())
            {
                states.StateProcced(this);
            }
      
    }
    public double Attack(Entity enemy, float multiplyDamageCoefficient)
    {

        if (!IsDodging(enemy.evasionChance))
        {
            float tempDamage;
            if (IsCriticalStrike())
            {
                tempDamage = this.damage * multiplyDamageCoefficient * this.criticalDamage;
            }
            else
            {
                tempDamage = this.damage * multiplyDamageCoefficient;
            }

            tempDamage -= enemy.protect;
            tempDamage = tempDamage > 0 ? tempDamage : 0;
            enemy.TakeDamage(tempDamage);
            return tempDamage;
        }
        return 0;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    protected bool IsCriticalStrike()
    {
        float random = UnityEngine.Random.Range(1, 100);
        if (criticalChance >= random)
        {

            return true;
        }
        return false;
    }

    protected bool IsDodging(float chance)
    {
        float random = UnityEngine.Random.Range(1, 100);
        if (chance >= random)
        {
            return true;
        }
        return false;

    }

    public void TakeDamage(float damage)
    {
        health = health - damage;
    }

    public AssetItem ReturnItemWhithThisType(ItemType needItemType)
    {
        foreach (var item in Items)
        {
            if(item.itemType == needItemType)
            {
                return item;
            }
        }
        return null;
    }

}

