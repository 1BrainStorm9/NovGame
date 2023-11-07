using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseTerritory : MonoBehaviour
{
    [SerializeField] private GameObject door;

    private CycleDayNight cycleDayNight;

    void Start()
    {
        cycleDayNight = FindObjectOfType<CycleDayNight>();
        if (cycleDayNight != null)
        {
            UpdateDoorState();
        }
    }

    void Update()
    {
        if (cycleDayNight != null)
        {
            UpdateDoorState();
        }
    }

    private void UpdateDoorState()
    {
        int timeInScene = cycleDayNight.getTime();
        EnumTime timeType = cycleDayNight.returnType(timeInScene);
        bool isNight = timeType == EnumTime.isSunset || timeType == EnumTime.isNight;
        door.SetActive(isNight);
    }
}
