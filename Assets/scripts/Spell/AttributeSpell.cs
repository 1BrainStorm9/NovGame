using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spell Attribute", menuName = "Spell/Attribute")]
public class AttributeSpell : ScriptableObject
{
    [Header("Description")]
    public string Name;
    public Sprite Icone;
    public bool CastForEnemy;
}
