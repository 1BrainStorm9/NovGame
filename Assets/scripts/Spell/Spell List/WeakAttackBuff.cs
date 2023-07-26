using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeakAttackBuff : Spell
{
    public AttributeSpell _attribute;

    public override AttributeSpell A_Attribute { get { return _attribute; } set { _attribute = value; } }


    public override void A_ActivateSpell(Entity target)
    {
        AttackBuffState AttackBuffState = target.GetComponent<AttackBuffState>();

        if (AttackBuffState == null)
        {
            AttackBuffState = target.AddComponent<AttackBuffState>();
            AttackBuffState.TurnsCount = 2;
            AttackBuffState.damageBuff = 3;
            target.CharStates.Add(AttackBuffState);
        }
        else
        {
            AttackBuffState.TurnsCount += 1;
        }
    }
}
