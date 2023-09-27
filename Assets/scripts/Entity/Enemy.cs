using PixelCrew.UI.Widgets;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : Creature
{
    private Creature target;

    public void EnemyTurn() 
    { 
        if(target != null)
        {
            var csc = GetComponent<CastSpellController>();

            if (!csc.isSpellSelected)
            {
                Attack(target, 1f);
                target.GetComponentInChildren<LifeBarWidget>().HpChanged(target.health);
            }
            else csc.UseSpell(target);
        }
    }

    public Creature ChooseSpellAndGetTarget()
    {
        ChooseSpell();
        return target;
    }

    private void ChooseSpell()
    {
        var castSpellController = GetComponent<CastSpellController>();
        var gc = FindObjectOfType<GameController>();

        if (uniqueBasicSpells == null || uniqueBasicSpells.Count <= 0)
        {
            ChoosePlayerTarget(gc.playerList);
            return;
        }

        castSpellController.SpellID = Random.Range(0, uniqueBasicSpells.Count);
        castSpellController.isSpellSelected = true;

        ChooseTarget(castSpellController, gc);
    }

    private void ChooseTarget(CastSpellController castSpellController, GameController gc)
    {

        if (castSpellController.Spells[castSpellController.SpellID].A_Attribute.CastForEnemy)
        {
            ChoosePlayerTarget(gc.playerList);
        }
        else
        {
            ChooseEnemyTarget(gc.enemyList);
        }
    }

    private void ChoosePlayerTarget(List<Hero> targets)
    {
        if (targets.Count == 0 || targets == null) return;
        int randomIndex = Random.Range(0, targets.Count);
        target = targets[randomIndex];
    }

    private void ChooseEnemyTarget(List<Enemy> targets)
    {
        if (targets.Count == 0 || targets == null) return;
        int randomIndex = Random.Range(0, targets.Count);
        target = targets[randomIndex];
    }

    
}
