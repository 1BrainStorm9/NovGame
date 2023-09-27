using PixelCrew.UI.Widgets;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealHP : Spell
{
    public AttributeSpell _attribute;
    public int HealCurrent = 3;
    public float maxHp;

    public override AttributeSpell A_Attribute { get { return _attribute; } set { _attribute = value; } }



    public override void A_ActivateSpell(Creature target)
    {
        if (target.maxHealth >= target.health + HealCurrent)
        {
            target.health += HealCurrent;
        }
        else
        {
            target.health = target.maxHealth;
        }

    }
}
