using UnityEngine;
using UnityEngine.InputSystem;

public class HumanMovementAMI : MonoBehaviour
{
    public float moveSpeed = 5f;

    Vector3 movement;

    public void OnMovement(InputValue value)
    {
        movement = value.Get<Vector3>();
    }

    void Update()
    {
        Vector3 move = transform.right * movement.x + transform.forward * movement.z;
        transform.Translate(move * moveSpeed * Time.deltaTime, Space.World);
    }
}