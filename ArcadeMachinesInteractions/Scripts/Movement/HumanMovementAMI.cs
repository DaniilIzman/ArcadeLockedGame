using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class HumanMovementAMI : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] InputActionAsset inputActions;

    [Header("Movement")]
    [SerializeField] float walkSpeed   = 5f;
    [SerializeField] float crouchSpeed = 2.5f;

    [Header("Crouch")]
    [SerializeField] float crouchTransitionSpeed = 8f;

    [Header("Ground Detection")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float groundCheckRadius   = 0.25f;
    [SerializeField] float groundCheckDistance = 0.3f;

    public bool isMoving    = false;
    public bool isCrouching = false;

    Rigidbody        rb;
    CapsuleCollider  col;
    HumanAudioAMI    humanAudio;

    InputAction moveAction;
    InputAction crouchAction;

    int groundContactCount = 0;

    float originalHeight;
    float originalCenterY;
    float originalRadius;

    void Awake()
    {
        rb         = GetComponent<Rigidbody>();
        col        = GetComponent<CapsuleCollider>();
        humanAudio = GetComponent<HumanAudioAMI>();

        rb.freezeRotation = true;
        rb.interpolation  = RigidbodyInterpolation.Interpolate;

        originalHeight  = col.height;
        originalCenterY = col.center.y;
        originalRadius  = col.radius;

        InputActionMap map = inputActions.FindActionMap("ArcadeRoom");
        moveAction   = map.FindAction("Movement");
        crouchAction = map.FindAction("Crouch");
    }

    void OnEnable()
    {
        moveAction.Enable();
        crouchAction.Enable();

        crouchAction.performed += OnCrouchToggle;
    }

    void OnDisable()
    {
        crouchAction.performed -= OnCrouchToggle;

        moveAction.Disable();
        crouchAction.Disable();
    }

    void OnCrouchToggle(InputAction.CallbackContext ctx)
    {
        if (!isCrouching)
        {
            isCrouching = true;
        }
        else
        {
            if (!BlockedAbove())
                isCrouching = false;
        }
    }

    void Update()
    {
        float targetHeight;
        float targetRadius;

        if (isCrouching)
        {
            targetHeight = originalHeight * 0.5f;
            targetRadius = originalRadius * 0.5f;
        }
        else
        {
            targetHeight = originalHeight;
            targetRadius = originalRadius;
        }

        col.height = Mathf.Lerp(col.height, targetHeight, Time.deltaTime * crouchTransitionSpeed);
        col.radius = Mathf.Lerp(col.radius, targetRadius, Time.deltaTime * crouchTransitionSpeed);
        col.center = new Vector3(0f, originalCenterY, 0f);

        if (humanAudio != null)
            humanAudio.Tick(isMoving, isCrouching);
    }

    void FixedUpdate()
    {
        UpdateGroundContacts();

        Vector3 raw    = moveAction.ReadValue<Vector3>();
        float   inputX = Mathf.Clamp(raw.x, -1f, 1f);
        float   inputZ = Mathf.Clamp(raw.z, -1f, 1f);

        Vector3 move = transform.right * inputX + transform.forward * inputZ;

        if (move.magnitude > 1f)
            move.Normalize();

        float speed;
        if (isCrouching)
            speed = crouchSpeed;
        else
            speed = walkSpeed;

        Vector3 safeMove = ClipVelocity(move, speed);

        rb.linearVelocity = new Vector3(safeMove.x, rb.linearVelocity.y, safeMove.z);

        isMoving = safeMove.sqrMagnitude > 0.001f;
    }

    Vector3 ClipVelocity(Vector3 direction, float speed)
    {
        Vector3 desired = direction * speed;

        if (desired.sqrMagnitude < 0.001f)
            return Vector3.zero;

        float   halfHeight    = (col.height * 0.5f) - col.radius;
        Vector3 colCenter     = transform.position + col.center;
        Vector3 capsuleTop    = colCenter + Vector3.up   * halfHeight;
        Vector3 capsuleBottom = colCenter + Vector3.down * halfHeight;

        float   castDist = speed * Time.fixedDeltaTime + 0.05f;
        Vector3 castDir  = new Vector3(desired.x, 0f, desired.z).normalized;

        RaycastHit hitInfo;
        bool hit = Physics.CapsuleCast(
            capsuleTop,
            capsuleBottom,
            col.radius,
            castDir,
            out hitInfo,
            castDist,
            ~0,
            QueryTriggerInteraction.Ignore
        );

        if (!hit)
            return desired;

        Vector3 normal = hitInfo.normal;
        normal.y       = 0f;
        normal         = normal.normalized;
        float dot      = Vector3.Dot(desired, normal);

        if (dot >= 0f)
            return desired;

        return desired - normal * dot;
    }

    void UpdateGroundContacts()
    {
        Vector3 castOrigin = transform.position + new Vector3(0f, originalCenterY, 0f);

        RaycastHit hitInfo;
        bool hit = Physics.SphereCast(
            castOrigin,
            groundCheckRadius,
            Vector3.down,
            out hitInfo,
            (originalCenterY - originalRadius) + groundCheckDistance,
            groundLayer,
            QueryTriggerInteraction.Ignore
        );

        if (hit)
            groundContactCount = 1;
        else
            groundContactCount = 0;
    }

    bool BlockedAbove()
    {
        Vector3 castOrigin = transform.position + new Vector3(0f, originalCenterY, 0f);
        float   castDist   = (originalHeight * 0.5f) + 0.1f;
        RaycastHit hitInfo;
        return Physics.SphereCast(
            castOrigin,
            originalRadius * 0.8f,
            Vector3.up,
            out hitInfo,
            castDist,
            ~0,
            QueryTriggerInteraction.Ignore
        );
    }

    public int GetGroundContactCount()
    {
        return groundContactCount;
    }
}