using UnityEngine;

public class CycleDayNight : MonoBehaviour
{
    [SerializeField] private float dayLengthInSeconds = 60.0f;
    [SerializeField] private Light sunLight;

    private void Update()
    {
        float timeF = Mathf.PingPong(Time.time / dayLengthInSeconds, 1.0f);
        float intensity = Mathf.Lerp(1.0f, 0.1f, timeF);
        sunLight.intensity = intensity;
    }
}
