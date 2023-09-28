using UnityEngine;

public class SpawnFireStation : MonoBehaviour
{
    [SerializeField] private GameObject prefabFireStation;
    [SerializeField] private GameObject circleForSpawn;

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
        if (Input.GetKeyUp(targetKey))
        {
            enableCircle = !enableCircle;
            circleForSpawn.SetActive(enableCircle);
            isPlace = !isPlace;
            Debug.Log("1");
        }

        if (isPlace && Input.GetMouseButtonDown(0) && firstFireStation)
        {
            Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            clickPosition.z = -6;
            Debug.Log("2");
            if (Vector3.Distance(clickPosition, circleForSpawn.transform.position) <= circleForSpawn.transform.localScale.x * 13)
            {

                Instantiate(prefabFireStation, clickPosition, Quaternion.identity);
                Debug.Log("3");
                firstFireStation = false;
            }

            
        }
    }
}
