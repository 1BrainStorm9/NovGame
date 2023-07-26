using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SpellHUDController : MonoBehaviour
{
    public List<Image> spellImages;
    public Entity activeEntity;
    public Sprite lockSprite;

    private CastSpellController _castSpellSystem;
    private GameController gameController;

    public void ReloadHUD()
    {
        gameController = FindObjectOfType<GameController>();
        activeEntity = gameController.GetActiveCreature;
        _castSpellSystem = activeEntity.GetComponent<CastSpellController>();

        ResetButtonsImages();

        for (int i = 0; i < _castSpellSystem.Spells.Count; i++)
        {
            var imageButton = spellImages[i].GetComponentInChildren<Image>();
            imageButton.sprite = _castSpellSystem.Spells[i].A_Attribute.Icone;
        }
    }

    public void ResetButtonsImages()
    {
        for (int i = 0; i < spellImages.Count; i++)
        {
            var imageButton = spellImages[i].GetComponentInChildren<Image>();
            imageButton.sprite = lockSprite;
        }
    }

}
