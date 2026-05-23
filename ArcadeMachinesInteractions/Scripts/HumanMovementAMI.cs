using UnityEngine;
using UnityEngine.InputSystem;

public class HumanMovementAMI : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float crouchSpeedMultiplier = 0.5f;

    [Header("Ground Detection")]
    [SerializeField] int groundContactCount = 0;

    Rigidbody rb;
    CapsuleCollider capsule;
    Vector3 movement;

    public bool isCrouching = false;
    float initialHeight;
    float crouchHeight;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        capsule = GetComponent<CapsuleCollider>();
        rb.freezeRotation = true;

        initialHeight = capsule.height;
        crouchHeight = initialHeight / 2f;

        if (PlayerPositionManager.instance.HasData())
        {
            transform.position = PlayerPositionManager.instance.GetSavedPosition();
            transform.rotation = PlayerPositionManager.instance.GetSavedRotation();
            
            rb.constraints = RigidbodyConstraints.None;
            rb.freezeRotation = true;
            
            PlayerPositionManager.instance.ClearData();
            
            Debug.Log("Player restored to position: " + transform.position);
        }
    }

    public void OnMovement(InputValue value)
    {
        movement = value.Get<Vector3>();
    }

    public void OnJump(InputValue value)
    {
        if (value.Get<float>() > 0.5f && IsGrounded())
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
        ApplyMovement();
    }

    void ApplyMovement()
    {
        Vector3 moveDirection = transform.right * movement.x + transform.forward * movement.z;
        
        float speed;
        if (isCrouching)
        {
            speed = moveSpeed * crouchSpeedMultiplier;
        }
        else
        {
            speed = moveSpeed;
        }

        if (IsGrounded())
        {
            rb.linearVelocity = new Vector3(moveDirection.x * speed, rb.linearVelocity.y, moveDirection.z * speed);
        }
        else
        {
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
        }
    }

    bool IsGrounded()
    {
        return groundContactCount > 0;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.isTrigger)
        {
            groundContactCount++;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (!collision.collider.isTrigger)
        {
            groundContactCount--;
        }
    }

    public int GetGroundContactCount()
    {
        return groundContactCount;
    }
}