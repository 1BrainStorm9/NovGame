using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraZoomZoneInHouse : MonoBehaviour
{
    [SerializeField] private GameObject objectToFade;
    [SerializeField] private float fadeSpeed;
    [SerializeField] private float zoomSpeed;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private float minOrthoSize;
    [SerializeField] private float maxOrthoSize;

    [SerializeField] private float cooldownTime;
    private bool isFading = false;
    private bool isFaded = false;
    private bool inTriggerZone = false;
    private float lastUsedTime;
    
    private Movment Movment; 
    private Animator animator;

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
            isFading = true;
            isFaded = false;
            UpdateCameraOrthoSize();
        }
    }

    private void Update()
    {
        if (inTriggerZone)
        {
            if (Input.GetKeyDown(KeyCode.E) && Time.time - lastUsedTime >= cooldownTime /*&& currentTime == EnumTime.isDay*/)
            {
                lastUsedTime = Time.time;
                isFaded = !isFaded;
                isFading = true;
                UpdateCameraOrthoSize();
            }
        }

        if (isFading)
        {
            Movment = FindObjectOfType<Movment>();
            Movment.enabled = false;
            animator = FindObjectOfType<Animator>();
            animator.Play("HeroIdle");

            Color currentColor = objectToFade.GetComponent<Renderer>().material.color;
            float targetAlpha = isFaded ? 0.0f : 1.0f;
            float newAlpha = Mathf.Lerp(currentColor.a, targetAlpha, fadeSpeed * Time.deltaTime);
            currentColor.a = newAlpha;
            objectToFade.GetComponent<Renderer>().material.color = currentColor;
            if (Mathf.Abs(newAlpha - targetAlpha) < 0.01f)
            {
                UpdateCameraOrthoSize();
                isFading = false;
                Movment.enabled = true;
                animator.Play("HeroIdle");
            }
        }
    }

    private void UpdateCameraOrthoSize()
    {
        float targetOrthoSize = isFaded ? minOrthoSize : maxOrthoSize;
        StartCoroutine(ChangeOrthoSize(targetOrthoSize));
    }

    private IEnumerator ChangeOrthoSize(float targetSize)
    {
        float startSize = virtualCamera.m_Lens.OrthographicSize;
        float timeElapsed = 0f;

        while (timeElapsed < zoomSpeed)
        {
            timeElapsed += Time.deltaTime;
            float t = timeElapsed / zoomSpeed;
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(startSize, targetSize, t);
            yield return null;
        }

        virtualCamera.m_Lens.OrthographicSize = targetSize;
    }
}
