using System;
using TMPro;
using UnityEngine;

public class CycleDayNight : MonoBehaviour
{
    [SerializeField] private float dayLengthInSeconds;
    [SerializeField] private int dayStartTime;
    [SerializeField] private Light sunLight;
    [SerializeField] private TextMeshProUGUI textMeshPro;
    private float timer;
    private int time;

    private float timerToLerp;
    private float timeToLerp;
    private bool isSunRiseOrSunSet;
    public static Action NewDayCome;

    private void Start()
    {
        time = dayStartTime;
    }

    private void FixedUpdate()
    {
        if(isSunRiseOrSunSet) timerToLerp += Time.fixedDeltaTime;
        if (timerToLerp > (dayLengthInSeconds / 24) * 3)
        {
            timerToLerp = 0;
        }

        timer += Time.fixedDeltaTime;
        if (timer > dayLengthInSeconds / 24)
        {
            timer = 0;
            time++;
        }
        if (time == 24)
        {
            NewDayCome?.Invoke();
            time = 0;

        }


        textMeshPro.text = "Time: " + time.ToString() + ".00";

        ChangeLightIntensity();
    }


    private void ChangeLightIntensity()
    {
        timeToLerp = timerToLerp / ((dayLengthInSeconds / 24) * 3);
        switch (returnType(time))
        {
            case EnumTime.isDay:
                isSunRiseOrSunSet = false;
                sunLight.intensity = 1;
                break;
            case EnumTime.isSunrise:
                isSunRiseOrSunSet = true;
                sunLight.intensity = Mathf.Lerp(0.1f, 1f, timeToLerp);
                break;
            case EnumTime.isSunset:
                isSunRiseOrSunSet = true;
                sunLight.intensity = Mathf.Lerp(1f, 0.1f, timeToLerp);
                break;
            case EnumTime.isEvening:
                isSunRiseOrSunSet = false;
                sunLight.intensity = 0.1f;
                break;
        }
    }

    public EnumTime returnType(int time)
    {

        if (time >= 2 && time < 5)
        {
            return EnumTime.isSunrise;
        }
        if (time < 20 && time >= 5)
        {
            return EnumTime.isDay;
        }
        if (time < 23 && time >= 20)
        {
            return EnumTime.isSunset;
        }
        if (time >= 23 || time < 2)
        {
            return EnumTime.isEvening;
        }

        return EnumTime.isDay; ;
    }
}


public enum EnumTime
{
    isSunrise, isDay, isSunset, isEvening,
}

