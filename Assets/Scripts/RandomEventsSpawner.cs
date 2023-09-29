using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEventsSpawner : MonoBehaviour
{
    [SerializeField] private int chanceAttackedInDay = 3;
    [SerializeField] private int chanceAttackedInNight = 10;

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
        if(cycleDayNight != null)
        {
            timeInScene = cycleDayNight.getTime();
        }

        isNight = (timeInScene >= 20 && timeInScene <= 23) || (timeInScene >= 0 && timeInScene < 2);

        if(timeInScene == 0 )
        {
            canBeAttacked = true;
        }

        if (!isNight)
        {
            if((Random.Range(0, 101) <= chanceAttackedInDay) && canBeAttacked)
            {
                Debug.Log("Вы атакованы днём!");
                canBeAttacked = !canBeAttacked;
            }
        }
        else
        {
            if ((Random.Range(0, 101) <= chanceAttackedInNight) && canBeAttacked)
            {
                Debug.Log("Вы атакованы ночью!");
                canBeAttacked = !canBeAttacked;
            }
        }

    }
}
