using System;
using TMPro;
using UnityEngine;

public class CycleDayNight : MonoBehaviour
{
    [SerializeField] private float dayLengthInSeconds;
    [SerializeField] private Light sunLight;
    [SerializeField] private TextMeshProUGUI textMeshPro;
    private float timer;
    private int time;

    private float timerToLerp;
    private float timeToLerp;
    private bool isSunRiseOrSunSet;
    public static Action NewDayCome;
    public static Action onAttacked;


    public Light GetLight() { return sunLight; }
    public int getTime() { return time; }


    private void FixedUpdate()
    {
        LerpTimerHandler();
        TimerHanler();
        ChangeLightIntensity();
        ChangeTimeText();
    }

    private void LerpTimerHandler()
    {
        if (isSunRiseOrSunSet) timerToLerp += Time.fixedDeltaTime;

        if ((timerToLerp > (dayLengthInSeconds / 24) * 3 + 1))
        {
            timerToLerp = 0;
        }
    }

    private void TimerHanler()
    {
        timer += Time.fixedDeltaTime;
        if (timer > dayLengthInSeconds / 24)
        {
            onAttacked?.Invoke();
            timer = 0;
            time++;
            ChangeTimeText();
        }
        if (time == 24)
        {
            NewDayCome?.Invoke();
            time = 0;
        }
    }

    private void ChangeTimeText()
    {
       //textMeshPro.text = "Time: " + time.ToString() + ".00";
    }


    private void ChangeLightIntensity()
    {
        timeToLerp = timerToLerp / ((dayLengthInSeconds / 24) * 3);
        switch (returnType(time))
        {
            case EnumTime.isDay:
                isSunRiseOrSunSet = false;
                sunLight.intensity = 1;
                timerToLerp = 0;
                timeToLerp = 0;
                break;
            case EnumTime.isNight:
                isSunRiseOrSunSet = false;
                sunLight.intensity = 0.1f;
                timerToLerp = 0;
                timeToLerp = 0;
                break;
            case EnumTime.isSunrise:
                isSunRiseOrSunSet = true;
                sunLight.intensity = Mathf.Lerp(0.1f, 1f, timeToLerp);
                break;
            case EnumTime.isSunset:
                isSunRiseOrSunSet = true;
                sunLight.intensity = Mathf.Lerp(1f, 0.1f, timeToLerp);
                break;
        }
    }

    public EnumTime returnType(int time)
    {
        EnumTime result;

        switch (time)
        {
            case int t when t >= 7 && t < 20:
                result = EnumTime.isDay;
                break;
            case int t when t >= 23 || t < 4:
                result = EnumTime.isNight;
                break;
            case int t when t >= 4 && t < 7:
                result = EnumTime.isSunrise;
                break;
            case int t when t >= 20 && t < 23:
                result = EnumTime.isSunset;
                break;
            default:
                result = EnumTime.isDay;
                break;
        }

        return result;
    }
}


public enum EnumTime
{
    isSunrise, isDay, isSunset, isNight,
}

[Serializable]
public class TimeOfDayData
{
    public int time;
    public float intensity;

    public TimeOfDayData(int time,float intensity)
    {
        this.time = time;
        this.intensity = intensity;
    }

}
