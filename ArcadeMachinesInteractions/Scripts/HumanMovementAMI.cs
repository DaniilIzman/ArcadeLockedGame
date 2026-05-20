using UnityEngine;
using UnityEngine.InputSystem;

public class HumanMovementAMI : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float crouchHeightOffset = 0.5f;

    Vector3 movement;
    Rigidbody rb;
    public bool isCrouching = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;  
    }

    public void OnMovement(InputValue value)
    {
        movement = value.Get<Vector3>();
    }

    public void OnJump(InputValue value)
    {
        if (value.Get<float>() > 0.5f)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            Debug.Log("Jump triggered!");
        }
    }

    public void OnCrouch(InputValue value)
    {
        if (value.Get<float>() > 0.5f)
        {
            isCrouching = true;   
            Debug.Log("Crouching: " + isCrouching);    
        }

        else
        {
            isCrouching = false;   
        }
    }

    void FixedUpdate()
    {
        Vector3 move = transform.right * movement.x + transform.forward * movement.z;
        Vector3 velocity = move * moveSpeed;
        
        rb.linearVelocity = new Vector3(velocity.x, rb.linearVelocity.y, velocity.z);
    }
}