using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using PixelCrew.Components.GoBased;
using System;

[Serializable]
public abstract class Creature : MonoBehaviour
{
    [Header("------Characteristics------")]
    [Space]
    public float damage;
    public float criticalDamage;
    public float criticalChance;
    public float health;
    public float protect;
    public float evasionChance;

    [Header("------States------")]
    [Space]
    public List<StateInfo> CharStates;

    [Header("------Items------")]
    [Space]
    public List<AssetItem> Items;
    public Weapon weapon;

    [Header("------Spells------")]
    [Space]
    public List<Spell> uniqueBasicSpells;

    [Header("------System info------")]
    [Space]
    public bool SpellsInHUD = false;
    public int fightIndex, listIndex;
    [SerializeField] protected SpawnListComponent _particles;
    protected Animator Animator;
    private static readonly int AttackKey = Animator.StringToHash("attack");
    private static readonly int Hit = Animator.StringToHash("hit");
    private static readonly int Die = Animator.StringToHash("is-dead");
    private static readonly int Evasion = Animator.StringToHash("evasion");

    private GameSession gameSession;



    protected void Awake()
    {
        Animator = GetComponent<Animator>();
    }

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
    public virtual double Attack(Creature enemy, float multiplyDamageCoefficient)
    {
        Animator.SetTrigger(AttackKey);
        if (!enemy.IsDodging())
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

    public void OnAttack()
    {
        _particles.Spawn("Slash");
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

    protected bool IsDodging()
    {
        float random = UnityEngine.Random.Range(1, 100);
        if (evasionChance >= random)
        {
            Animator.SetTrigger(Evasion);
            return true;
        }
        return false;

    }

    public void TakeDamage(float damage)
    {

        health = health - damage;

        if(health <= 0)
        {
            OnDie();
            return;
        }
        Animator.SetTrigger(Hit);
    }

    public void OnDie()
    {
        Animator.SetBool(Die, true);
        Invoke("Destroy", 1f);
    }

    public void Destroy()
    {
        Destroy(gameObject);
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

