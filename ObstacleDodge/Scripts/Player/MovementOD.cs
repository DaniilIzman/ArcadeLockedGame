using UnityEngine;

public class MovementOD : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 120f;
    public bool canMove = true;

    private Rigidbody rb;
    private float moveInput;
    private float turnInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void Update()
    {
        if (!canMove) 
        {
            moveInput = 0f;
            turnInput = 0f;
            return;
        }

        moveInput = Input.GetAxisRaw("Vertical");
        turnInput = Input.GetAxisRaw("Horizontal");
    }

    void FixedUpdate()
    {
        if (!canMove) return;

        Turn();
        Move();
    }

    private void Move()
    {
        // If the player is actively turning, stop forward movement and kill momentum
        if (Mathf.Abs(turnInput) > 0.01f)
        {
            rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f);
            return;
        }

        Vector3 targetVelocity = transform.forward * moveInput * moveSpeed;
        targetVelocity.y = rb.linearVelocity.y; 
        rb.linearVelocity = targetVelocity;
    }

    private void Turn()
    {
        float turn = turnInput * rotationSpeed * Time.fixedDeltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);
    }

    public void ApplySpeedMultiplier(float multiplier)
    {
        moveSpeed *= multiplier;
    }
}