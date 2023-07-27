using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CastSpellController : MonoBehaviour
{
    public bool SpellSelected = false;
    public int SpellID = -1;
    public List<Spell> Spells;
    public List<Spell> BasicSpells;
    private void Start()
    {
        BasicSpells.AddRange(GetComponent<Entity>().uniqueBasicSpells);
        Spells.Clear();
        Spells.AddRange(BasicSpells);
    }

    public void SetActiveSpells(List<Spell> spells)
    {
        Spells.AddRange(spells);
    }


    public bool CanCastForEnemy()
    {
        return Spells[SpellID].A_Attribute.CastForEnemy;
    }

    public void UseSpell(Entity targetObject)
    {
        if (SpellSelected)
        {
            Spells[SpellID].A_ActivateSpell(targetObject);
            SpellSelected = false;
        }
    }

    public void OnButtonClick()
    {
        Button button = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        ButtonInfo buttonInfo = button.GetComponent<ButtonInfo>();
        int index = buttonInfo.index;
        SpellID = index;
        SpellSelected = true;
    }
}
