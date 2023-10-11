using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        cycleDayNight.SetTime(gameSession.saveTime);
    }

    void Update()
    {
        
    }
}
