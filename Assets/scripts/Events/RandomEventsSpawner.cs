using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomEventsSpawner : MonoBehaviour
{
    [SerializeField] private int chanceAttackedInDay;
    [SerializeField] private int chanceAttackedInNight;

    private bool isNight = false;
    private bool canBeAttacked = true;
    private static int timeInScene;

    private GameSession gameSession;

    private void OnEnable()
    {
        CycleDayNight.onAttacked += AttackedAPlayer;
    }

    private void OnDisable()
    {
        CycleDayNight.onAttacked -= AttackedAPlayer;
    }

    private void AttackedAPlayer()
    {
        gameSession = FindObjectOfType<GameSession>();

        CycleDayNight cycleDayNight = FindObjectOfType<CycleDayNight>();
        if (cycleDayNight != null)
        {
            timeInScene = cycleDayNight.getTime();
        }

        var timeType = cycleDayNight.returnType(timeInScene);
        isNight = timeType == EnumTime.isSunset || timeType == EnumTime.isNight;

        if (timeInScene == 0)
        {
            canBeAttacked = true;
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            if (!isNight)
            {
                if ((Random.Range(0, 101) <= chanceAttackedInDay) && canBeAttacked)
                {
                    gameSession.savedPlayerPosition = player.transform.position;
                    SceneManager.LoadScene("Pole");
                    gameSession.previousSceneName = "Pole";
                    canBeAttacked = !canBeAttacked;
                }
            }
            else
            {
                if ((Random.Range(0, 101) <= chanceAttackedInNight) && canBeAttacked)
                {
                    gameSession.savedPlayerPosition = player.transform.position;
                    SceneManager.LoadScene("Pole");
                    gameSession.previousSceneName = "Pole";
                    canBeAttacked = !canBeAttacked;
                }
            }
        }
    }

}
