using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject selectCircle;
    public GameObject targetCircle;
    public GameObject teamTargetCircle;
    public SpellHUDController spellBar;
    public GameObject hudCanvas;
    public GameObject endGamePanel;
    public GameObject gameResult;

    private void Start()
    {
        spellBar = FindObjectOfType<SpellHUDController>();
    }

    public void ShowWinEndGamePanel()
    {   
        endGamePanel.SetActive(true);
        gameResult.GetComponent<TextMeshProUGUI>().text = "Вы победили!";
    }

    public void ShowLoseEndGamePanel()
    {
        endGamePanel.SetActive(true);
        gameResult.GetComponent<TextMeshProUGUI>().text = "Вы проиграли ;c";
    }


    public void HideEndGamePanel()
    {
        endGamePanel.SetActive(false);
    }

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