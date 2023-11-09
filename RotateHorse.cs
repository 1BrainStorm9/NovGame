using UnityEngine;

public class RotateHorse : MonoBehaviour
{
    public GameObject characterObject;
    public float rotationSpeed = 1f;

    void Update()
    {
        float currentRotation = characterObject.transform.eulerAngles.z;

        // ѕровер€ем, есть ли нажатие клавиши "A" (влево)
        if (Input.GetKey(KeyCode.A))
        {
            float targetRotation = currentRotation + rotationSpeed;

            characterObject.transform.rotation = Quaternion.Euler(0f, 0f, targetRotation);
        }

        // ѕровер€ем, есть ли нажатие клавиши "D" (вправо)
        else if (Input.GetKey(KeyCode.D))
        {
            float targetRotation = currentRotation - rotationSpeed;

            characterObject.transform.rotation = Quaternion.Euler(0f, 0f, targetRotation);
        }

        if (Input.GetKey(KeyCode.W))
        {
            Vector3 moveDirection = Quaternion.Euler(0f, 0f, currentRotation) * Vector3.up;

            characterObject.transform.Translate(moveDirection * 50 * Time.deltaTime, Space.World);
        }
    }
}
