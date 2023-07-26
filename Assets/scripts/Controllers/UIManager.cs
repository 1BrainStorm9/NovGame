using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject selectCircle;
    public GameObject targetCircle;
    public GameObject teamTargetCircle;
    public GameObject spellBar;
    public GameObject hudCanvas;

    public void ShowSelectCircle()
    {
        selectCircle.SetActive(true);
    }

    public void HideSelectCircle()
    {
        selectCircle.SetActive(false);
    }

    public void ShowTargetCircle()
    {
        targetCircle.SetActive(true);
    }

    public void HideTargetCircle()
    {
        targetCircle.SetActive(false);
    }

    public void ShowTeamTargetCircle()
    {
        teamTargetCircle.SetActive(true);
    }

    public void HideTeamTargetCircle()
    {
        teamTargetCircle.SetActive(false);
    }

    public void ShowHUD()
    {
        hudCanvas.SetActive(true);
        spellBar.SetActive(true);
    }

    public void HideHUD()
    {
        hudCanvas.SetActive(false);
        spellBar.SetActive(false);
    }

    public void SetSelectCirclePosition(Vector3 position)
    {
        selectCircle.transform.position = position;
    }

    public void SetTargetCirclePosition(Vector3 position)
    {
        targetCircle.transform.position = position;
    }

    public void SetTeamTargetCirclePosition(Vector3 position)
    {
        teamTargetCircle.transform.position = position;
    }

    public void ReloadSpellHUD()
    {
        spellBar.GetComponent<SpellHUDController>().ReloadHUD();
    }
}