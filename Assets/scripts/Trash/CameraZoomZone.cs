using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using static GameSession;

public class CameraZoomZone : MonoBehaviour
{
    [SerializeField] private float zoomSize;
    [SerializeField] private float zoomSpeed;
    private Camera mainCamera;
    private GameSession gameSession;
    private float initialSize;
    private float targetSize;

    void Start()
    {
        mainCamera = Camera.main;
        initialSize = mainCamera.orthographicSize;
        targetSize = initialSize;
        gameSession = FindObjectOfType<GameSession>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (CompareTag("ZoomZoneInYard"))
        {
            targetSize = zoomSize;
        }
        switch(gameSession.questsCounter)
        {
            case 0:
                Quest quest = new Quest();
                gameSession.quests = new List<Quest>
                {
                    new Quest { questName = "Move", isCompleted = false }
                };
                break;
            case 1:
                gameSession.quests = new List<Quest>
                {
                    new Quest { questName = "VisitNewLocation", isCompleted = false }
                };
                break;
            case 2:
                gameSession.quests = new List<Quest>
                {
                    new Quest { questName = "WTH MAZAFAKA", isCompleted = false }
                };
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (CompareTag("ZoomZoneInYard"))
        {
            targetSize = initialSize;
        }
    }

    void Update()
    {
        mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, targetSize, zoomSpeed * Time.deltaTime);
    }
}
