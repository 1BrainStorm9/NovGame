using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomZone : MonoBehaviour
{
    [SerializeField] private float zoomSize;
    [SerializeField] private float zoomSpeed;
    private Camera mainCamera;
    private float initialSize;
    private float targetSize;

    void Start()
    {
        mainCamera = Camera.main;
        initialSize = mainCamera.orthographicSize;
        targetSize = initialSize;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (CompareTag("ZoomZoneInYard"))
        {
            targetSize = zoomSize;
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
