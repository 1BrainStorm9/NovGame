using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadScript : MonoBehaviour
{
    private CycleDayNight cycleDayNight;
    private GameSession gameSession;
    private QuestManager questManager;


    void Start()
    {
        cycleDayNight = FindObjectOfType<CycleDayNight>();
        gameSession = FindObjectOfType<GameSession>();
        FindObjectOfType<Light>().intensity = gameSession.DataTime.intensity;
        questManager = FindObjectOfType<QuestManager>();

    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            questManager.ActivateQuest("ExitLocation");
        }
    }
}
