using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovementCF : MonoBehaviour
{
    [SerializeField] float controlSpeed = 0f;
    [SerializeField] float xRestrictionRange = 0f;
    [SerializeField] float yRestrictionRange = 0f;

    [SerializeField] float controlRotation = 0f;
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
        float xOffset = movement.x * controlSpeed * Time.deltaTime;
        float pureXpos = transform.localPosition.x + xOffset;
        float clampXpos = Mathf.Clamp(pureXpos, -xRestrictionRange, xRestrictionRange);

        float yOffset = movement.y * controlSpeed * Time.deltaTime;
        float pureYpos = transform.localPosition.y + yOffset;
        float clampYpos = Mathf.Clamp(pureYpos,-yRestrictionRange, yRestrictionRange);
        transform.localPosition = new Vector3(clampXpos, clampYpos, 0f);
    }

    void PlayerRotation()
    {
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, controlRotation * movement.x);
        transform.localRotation = targetRotation;
    }
}
