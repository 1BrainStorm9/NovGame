using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpellButtonsController : MonoBehaviour
{
    public Entity selectObject;
    private GameController gameController;

    private void Awake()
    {
        gameController = FindObjectOfType<GameController>();
    }
    public void OnButtonClick()
    {
        selectObject = gameController.GetActiveCreature;
        var castSpellController = selectObject.GetComponent<CastSpellController>();

        Button button = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        ButtonInfo buttonInfo = button.GetComponent<ButtonInfo>();

        int index = buttonInfo.index;
        if(castSpellController.Spells.Count > index)
        {
            castSpellController.SpellID = index;
            castSpellController.SpellSelected = true;
            gameController.manager.HideTargetCircle();
            gameController.manager.HideTeamTargetCircle();
        }
    }
}
