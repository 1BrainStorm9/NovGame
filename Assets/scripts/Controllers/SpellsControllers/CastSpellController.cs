using PixelCrew.UI.Widgets;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class CastSpellController : MonoBehaviour
{
    public bool isSpellSelected = false;
    public int SpellID = -1;
    public List<Spell> Spells;
    public List<Spell> BasicSpells;
    private void Start()
    {
        BasicSpells.AddRange(GetComponent<Creature>().uniqueBasicSpells);
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

    public void UseSpell(Creature targetObject )
    {
        if (isSpellSelected)
        {
            Spells[SpellID].A_ActivateSpell(targetObject);
            targetObject.GetComponentInChildren<LifeBarWidget>().HpChanged(targetObject.health);
            isSpellSelected = false;
        }
    }

    public void OnButtonClick()
    {
        Button button = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        ButtonInfo buttonInfo = button.GetComponent<ButtonInfo>();
        int index = buttonInfo.index;
        SpellID = index;
        isSpellSelected = true;
    }
}
