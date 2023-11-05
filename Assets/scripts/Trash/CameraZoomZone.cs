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
    public QuestManager questManager;
    private float initialSize;
    private float targetSize;

    void Start()
    {
        mainCamera = Camera.main;
        initialSize = mainCamera.orthographicSize;
        targetSize = initialSize;
        questManager = FindObjectOfType<QuestManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (CompareTag("ZoomZoneInYard"))
        {
            targetSize = zoomSize;
        }
        questManager.ActivateQuest("Move");
        questManager.ActivateQuest("VisitNewLocation");

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
