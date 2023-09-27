/*using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public float dayLengthInSeconds = 60.0f;
    public Light sunLight;

    private float timeOfDay = 0.0f;

    private void Update()
    {
        timeOfDay += Time.deltaTime / dayLengthInSeconds;

        if (timeOfDay > 1.0f)
        {
            timeOfDay -= 1.0f;
        }

        float angle = timeOfDay * 360.0f;
        sunLight.transform.rotation = Quaternion.Euler(angle, 0, 0);
    }
}
*/