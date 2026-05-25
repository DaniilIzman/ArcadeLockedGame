using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementCF : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float positionSpeed = 0f;
    [SerializeField] float xRestrictionRange = 0f;
    [SerializeField] float yRestrictionRange = 0f;

    [Header("Rotation")]
    [SerializeField] float pitchFactor = 0f;
    [SerializeField] float rollFactor = 0f;
    [SerializeField] float rotationSpeed = 0f;

    Vector2 movement;

    void Update()
    {
        PlayerPosition();
        PlayerRotation();
    }

    public void OnMove(InputValue value)
    {
        movement = value.Get<Vector2>();
    }

    void PlayerPosition()
    {
        float xOffset = movement.x * positionSpeed * Time.deltaTime;

        float pureXpos = transform.localPosition.x + xOffset;

        float clampXpos = Mathf.Clamp(pureXpos, -xRestrictionRange, xRestrictionRange);

        float yOffset = movement.y * positionSpeed * Time.deltaTime;

        float pureYpos = transform.localPosition.y + yOffset;

        float clampYpos = Mathf.Clamp(pureYpos, -yRestrictionRange, yRestrictionRange);

        transform.localPosition = new Vector3(clampXpos, clampYpos, 0f);
    }

    void PlayerRotation()
    {
        float roll = -rollFactor * movement.x;
        float pitch = -pitchFactor * movement.y;

        Quaternion targetRotation = Quaternion.Euler(pitch, 0f, roll);

        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}