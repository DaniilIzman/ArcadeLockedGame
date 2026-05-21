using UnityEngine;

public class HumanCameraAMI : MonoBehaviour
{
    [Header("Mouse Settings")]
    public float mouseSensitivityX = 100f;
    public float mouseSensitivityY = 100f;

    [Header("Camera Limits")]
    public float angleLimit = 90f;

    [Header("References")]
    public Transform playerBody;

    float xRotation = 0f;

    HumanMovementAMI movement;

    Vector3 originalLocalPosition;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        movement = playerBody.GetComponent<HumanMovementAMI>();

        originalLocalPosition = transform.localPosition;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivityX * Time.deltaTime;

        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivityY * Time.deltaTime;

        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -angleLimit, angleLimit);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        playerBody.Rotate(Vector3.up * mouseX);

        UpdateCrouchCamera();
    }

    void UpdateCrouchCamera()
    {
        Vector3 newPosition = originalLocalPosition;

        if (movement.isCrouching)
        {
            newPosition.y -= 0.5f;
        }

        transform.localPosition = newPosition;
    }
}