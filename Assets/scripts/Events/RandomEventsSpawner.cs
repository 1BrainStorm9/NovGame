using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEventsSpawner : MonoBehaviour
{
    [SerializeField] private int chanceAttackedInDay;
    [SerializeField] private int chanceAttackedInNight;

    private bool isNight = false;
    private bool canBeAttacked = true;
    private static int timeInScene;

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
        CycleDayNight cycleDayNight = FindObjectOfType<CycleDayNight>();
        if (cycleDayNight != null)
        {
            timeInScene = cycleDayNight.getTime();
        }

        var timeType = cycleDayNight.returnType(timeInScene);
        isNight = timeType == EnumTime.isSunset || timeType == EnumTime.isNight;

        if(timeInScene == 0 )
        {
            canBeAttacked = true;
        }

        if (!isNight)
        {
            if((Random.Range(0, 101) <= chanceAttackedInDay) && canBeAttacked)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Pole");
                canBeAttacked = !canBeAttacked;
            }
        }
        else
        {
            if ((Random.Range(0, 101) <= chanceAttackedInNight) && canBeAttacked)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Pole");
                canBeAttacked = !canBeAttacked;
            }
        }

    }
}
