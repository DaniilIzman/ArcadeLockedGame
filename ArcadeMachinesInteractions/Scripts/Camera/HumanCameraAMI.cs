using UnityEngine;
using UnityEngine.InputSystem;

public class HumanCameraAMI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform playerBody;

    [Header("Look Settings")]
    [SerializeField] float mouseSensitivity = 0.15f;

    [Tooltip("X = look-up limit, Y = look-down limit (positive values)")]
    [SerializeField] Vector2 pitchLimits = new Vector2(80f, 80f);

    [Tooltip("Horizontal clamp in degrees. 0 = unlimited.")]
    [SerializeField] float yawLimit = 0f;

    [Header("Crouch Camera")]
    [SerializeField] float crouchCameraOffset = 0.5f;
    [SerializeField] float crouchCameraSpeed  = 8f;

    HumanMovementAMI movement;
    Vector3 originalLocalPosition;

    float pitch = 0f;
    float yaw   = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible   = false;

        if (playerBody != null)
            movement = playerBody.GetComponentInParent<HumanMovementAMI>();

        if (movement == null)
            movement = GetComponentInParent<HumanMovementAMI>();

        originalLocalPosition = transform.localPosition;

        if (playerBody != null)
            yaw = playerBody.eulerAngles.y;
    }

    void Update()
    {
        UpdateLook();
        UpdateCrouchCamera();
    }

    void UpdateLook()
    {
        if (Mouse.current == null) return;

        Vector2 delta = Mouse.current.delta.ReadValue();

        float mouseX = delta.x * mouseSensitivity;
        float mouseY = delta.y * mouseSensitivity;

        pitch -= mouseY;
        pitch  = Mathf.Clamp(pitch, -pitchLimits.x, pitchLimits.y);

        if (yawLimit > 0f)
        {
            yaw += mouseX;
            yaw  = Mathf.Clamp(yaw, -yawLimit, yawLimit);
            transform.localRotation = Quaternion.Euler(pitch, yaw, 0f);
        }
        else
        {
            yaw += mouseX;
            if (playerBody != null)
                playerBody.rotation = Quaternion.Euler(0f, yaw, 0f);

            transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
        }
    }

    void UpdateCrouchCamera()
    {
        Vector3 target = originalLocalPosition;

        if (movement != null && movement.isCrouching)
            target.y -= crouchCameraOffset;

        transform.localPosition = Vector3.Lerp(
            transform.localPosition,
            target,
            Time.deltaTime * crouchCameraSpeed
        );
    }

    public void ResetCamera()
    {
        pitch = 0f;

        if (playerBody != null)
            yaw = playerBody.eulerAngles.y;
        else
            yaw = 0f;

        transform.localRotation = Quaternion.identity;
        transform.localPosition = originalLocalPosition;
    }
}