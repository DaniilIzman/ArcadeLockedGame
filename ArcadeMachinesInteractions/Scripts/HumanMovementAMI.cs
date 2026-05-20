using UnityEngine;
using UnityEngine.InputSystem;

public class HumanMovementAMI : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;

    Vector3 movement;
    Rigidbody rb;
    CapsuleCollider capsule;
    public bool isCrouching = false;
    
    float initialHeight;
    float crouchHeight;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        
        capsule = GetComponent<CapsuleCollider>();
        initialHeight = capsule.height;
        crouchHeight = initialHeight / 2f;
        
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
        }
    }

    public void OnCrouch(InputValue value)
    {
        isCrouching = value.Get<float>() > 0.5f;
        
        if (isCrouching)
        {
            capsule.height = crouchHeight;
        }
        else
        {
            capsule.height = initialHeight;
        }
    }

    void FixedUpdate()
    {
        Vector3 move = transform.right * movement.x + transform.forward * movement.z;
        Vector3 velocity = move * moveSpeed;
        
        rb.linearVelocity = new Vector3(velocity.x, rb.linearVelocity.y, velocity.z);
    }
}