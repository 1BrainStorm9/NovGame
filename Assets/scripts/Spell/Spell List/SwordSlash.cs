using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SwordSlash : Spell
{
    public AttributeSpell _attribute;
    public override AttributeSpell A_Attribute { get { return _attribute; } set { _attribute = value; } }

    public override void A_ActivateSpell(Creature target)
    {
        var select = FindObjectOfType<GameController>().GetActiveCreature;
        select.GetComponent<Creature>().Attack(target, 2f);
    }
}
