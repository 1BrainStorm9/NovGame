using UnityEngine;

public class CameraController : MonoBehaviour
{
    private bool _isBossFight;
    private float cameraSpeed = 0f;
    private float _moveAmount;
    [SerializeField] private ColliderCreationController enemyColiderController;
    public delegate void IsOnTriggerEnterEvent();
    public static event IsOnTriggerEnterEvent OnTriggerEnter;
    public bool getIsBossFight => _isBossFight;

    private void OnEnable()
    {
        GameController.OnKillAllEnemies += SetCameraSpeed;
    }

    private void OnDisable()
    {
        GameController.OnKillAllEnemies -= SetCameraSpeed;
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

            var gameController = FindObjectOfType<GameController>();
            gameController.manager.ShowSelectCircle();
        }
    }
}
