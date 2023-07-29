using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealHP : Spell
{
    public AttributeSpell _attribute;
    public int HealCurrent = 3;

    public override AttributeSpell A_Attribute { get { return _attribute; } set { _attribute = value; } }


    public override void A_ActivateSpell(Creature target)
    {
        target.health += HealCurrent;
    }
}
