using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadScript : MonoBehaviour
{
    private CycleDayNight cycleDayNight;
    private GameSession gameSession;

    void Start()
    {
        cycleDayNight = FindObjectOfType<CycleDayNight>();
        gameSession = FindObjectOfType<GameSession>();
        FindObjectOfType<Light>().intensity = gameSession.DataTime.intensity;
    }

}
