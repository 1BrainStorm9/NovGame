using Unity.VisualScripting;
using UnityEngine;

public class PoisonTouch : Spell
{
    public AttributeSpell _attribute;
    public override AttributeSpell A_Attribute { get { return _attribute; } set { _attribute = value; } }

    public override void A_ActivateSpell(Entity target)
    {
        var select = FindObjectOfType<GameController>().GetActiveCreature;
        select.GetComponent<Entity>().Attack(target,1.3f);
        var tempPoisonState = target.GetComponent<TemporalDamageState>();

        if (tempPoisonState == null)
        {
            TemporalDamageState PoisonState = target.AddComponent<TemporalDamageState>();
            PoisonState.TurnsCount = 2;
            PoisonState.temporalDamage = 3;
            target.CharStates.Add(PoisonState);
        }
        else
        {
            tempPoisonState.TurnsCount += 1;
        }
    }
}
