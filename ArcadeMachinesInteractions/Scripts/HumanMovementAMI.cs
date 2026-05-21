using UnityEngine;
using UnityEngine.InputSystem;

public class HumanMovementAMI : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float crouchSpeedMultiplier = 0.5f;

    Vector3 movement;
    Rigidbody rb;
    CapsuleCollider capsule;
    public bool isCrouching = false;
    bool isGrounded = false;
    
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
        if (value.Get<float>() > 0.5f && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            Debug.Log("Jumped!");
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
        
        float currentSpeed;
        if (isCrouching)
        {
            currentSpeed = moveSpeed * crouchSpeedMultiplier;
        }
        else
        {
            currentSpeed = moveSpeed;
        }
        
        Vector3 velocity = move * currentSpeed;
        
        if (isGrounded)
        {
            rb.linearVelocity = new Vector3(velocity.x, rb.linearVelocity.y, velocity.z);
        }
        else
        {
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (!collision.collider.isTrigger)
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (!collision.collider.isTrigger)
        {
            isGrounded = false;
        }
    }
}