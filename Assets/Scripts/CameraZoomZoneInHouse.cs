using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomZoneInHouse : MonoBehaviour
{
    [SerializeField] private GameObject objectToFade;
    [SerializeField] private float fadeSpeed;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float zoomSpeed;

    private bool isFading = false;
    private bool isFaded = false;
    private bool inTriggerZone = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (CompareTag("ZoomZoneInHouse"))
        {
            inTriggerZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (CompareTag("ZoomZoneInHouse"))
        {
            inTriggerZone = false;

            if (isFading)
            {
                isFaded = false;
            }
        }
    }

    private void Update()
    {
        if (inTriggerZone)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                isFaded = !isFaded;
                isFading = true;
            }
        }
        else
        {
            isFading = true; isFaded = false;
        }

        if (isFading)
        {
            Color currentColor = objectToFade.GetComponent<Renderer>().material.color;
            float targetAlpha = isFaded ? 0.0f : 1.0f;
            float newAlpha = Mathf.Lerp(currentColor.a, targetAlpha, fadeSpeed * Time.deltaTime);
            currentColor.a = newAlpha;
            objectToFade.GetComponent<Renderer>().material.color = currentColor;
            if (Mathf.Abs(newAlpha - targetAlpha) < 0.01f)
            {
                isFading = false;
            }

            float targetOrthographicSize = isFaded ? 3.0f : 7.0f;
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, targetOrthographicSize, zoomSpeed * Time.deltaTime);
        }
    }
}
