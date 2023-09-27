using System;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private bool _isBossFight;
    private float cameraSpeed = 0f;
    private float _moveAmount;
    [SerializeField] private ColliderCreationController enemyColiderController;

    public static Action OnTriggerEnter;

    public bool getIsBossFight => _isBossFight;

    private void OnEnable()
    {
        GameController.OnKilledAllEnemies += SetCameraSpeed;
    }

    private void OnDisable()
    {
        GameController.OnKilledAllEnemies -= SetCameraSpeed;
    }

    private void SetCameraSpeed()
    {
        cameraSpeed = 5;
    }

    private void FixedUpdate()
    {
        _moveAmount = cameraSpeed * Time.deltaTime;
        transform.position += Vector3.right * _moveAmount;
    }



    private void OnTriggerEnter2D (Collider2D other)
    {
        if (other.CompareTag("Trigger"))
        {
            other.isTrigger = true;

            enemyColiderController.AddCollider(transform);

            _isBossFight = other.GetComponent<TriggerInfo>().bossFight;
            cameraSpeed = 0;
            OnTriggerEnter();
        }
    }
}
