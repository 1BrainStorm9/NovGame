using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : MonoBehaviour
{
    public virtual AttributeSpell A_Attribute {get; set;}
    public virtual void A_ActivateSpell(Creature target)
    {
        Debug.Log("Activate");
    }
}
