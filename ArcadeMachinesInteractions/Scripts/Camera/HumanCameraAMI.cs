using UnityEngine;

public class HumanCameraAMI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform playerBody;

    HumanMovementAMI movement;
    Vector3 originalLocalPosition;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        movement = playerBody.GetComponent<HumanMovementAMI>();
        originalLocalPosition = transform.localPosition;

        ResetCamera();
    }

    void Update()
    {
        transform.localRotation = Quaternion.identity;

        UpdateCrouchCamera();
    }

    void UpdateCrouchCamera()
    {
        Vector3 newPosition = originalLocalPosition;

        if (movement != null && movement.isCrouching)
        {
            newPosition.y -= 0.5f;
        }

        transform.localPosition = newPosition;
    }

    public void ResetCamera()
    {
        transform.localRotation = Quaternion.identity;
        transform.localPosition = originalLocalPosition;
    }
}