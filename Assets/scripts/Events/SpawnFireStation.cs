using UnityEngine;

public class SpawnFireStation : MonoBehaviour
{
    [SerializeField] private GameObject prefabFireStation;
    [SerializeField] private GameObject circleForSpawn;
    private GameObject fireStation;

    private bool isPlace = false;
    private bool enableCircle = false;
    private bool firstFireStation = true;
    private KeyCode targetKey = KeyCode.L;


    private void Start()
    {
        circleForSpawn.SetActive(enableCircle);
    }

    private void Update()
    {

        var dayController = FindObjectOfType<CycleDayNight>();
        var enumTime = dayController.returnType(dayController.getTime());
        if (enumTime == EnumTime.isNight || enumTime == EnumTime.isSunset)
        {
            if (Input.GetKeyUp(targetKey))
            {
                enableCircle = !enableCircle;
                circleForSpawn.SetActive(enableCircle);
                isPlace = !isPlace;
            }
            if (isPlace && Input.GetMouseButtonDown(0) && firstFireStation)
            {
                Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                clickPosition.z = -6;
                if (Vector3.Distance(clickPosition, circleForSpawn.transform.position) <= circleForSpawn.transform.localScale.x * 13)
                {
                    fireStation = Instantiate(prefabFireStation, clickPosition, Quaternion.identity);
                    firstFireStation = false;
                }
            }
        }
        else
        {
            EnableCircle();
            if (fireStation != null)
            {
                Destroy(fireStation);

            }
        }
    }

    private void EnableCircle()
    {
        enableCircle = false;
        circleForSpawn.SetActive(enableCircle);
        isPlace = false;
        firstFireStation = true;
    }
}
