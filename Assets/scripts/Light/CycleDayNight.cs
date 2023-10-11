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
    public static Action onAttacked;
    private GameSession gameSession;

    private void Awake()
    {
        gameSession = FindObjectOfType<GameSession>();
    }

    public int getTime() { return time; }


    private void Start()
    {
        time = dayStartTime;
    }

    private void FixedUpdate()
    {
        LerpTimerHandler();
        TimerHanler();
        ChangeLightIntensity();
    }

    private void LerpTimerHandler()
    {
        if (isSunRiseOrSunSet) timerToLerp += Time.fixedDeltaTime;

        if (timerToLerp > (dayLengthInSeconds / 24) * 3)
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
        textMeshPro.text = "Time: " + time.ToString() + ".00";
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
            case EnumTime.isNight:
                isSunRiseOrSunSet = false;
                sunLight.intensity = 0.1f;
                break;
            case EnumTime.isSunrise:
                isSunRiseOrSunSet = true;
                sunLight.intensity = Mathf.Lerp(0.1f, 1f, timeToLerp);
                sunLight.intensity = Mathf.Lerp(0.1f, 1f, timeToLerp);
                break;
            case EnumTime.isSunset:
                isSunRiseOrSunSet = true;
                sunLight.intensity = Mathf.Lerp(1f, 0.1f, timeToLerp);
                break;
        }
    }

    public void SetTime(int newTime)
    {
        time = newTime;
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

