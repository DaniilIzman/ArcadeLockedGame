using UnityEngine;

public class DropperObstacle : MonoBehaviour
{
    [SerializeField] bool FallAfterTimer;
    [SerializeField] float TimeBeforeFall = 0f;
    [SerializeField] bool StaysOnTheGround;
    [SerializeField] float GroundTimer = 0f;
    TriggerHazard Trigger;
    MeshRenderer myMeshRenderer;
    float StartTimerBeforeFall;
    bool OnGround = false;

    Rigidbody myRigidBody;
    void Awake()
    {
        Trigger = FindAnyObjectByType<TriggerHazard>();
        if(Trigger.GameObjectList.Contains(gameObject))
        {
            gameObject.SetActive(false); 
        }
    }
    void Start()
    {
        myMeshRenderer = GetComponent<MeshRenderer>();
        myRigidBody = GetComponent<Rigidbody>();
        TriggerHazard[] triggers = FindObjectsByType<TriggerHazard>(FindObjectsSortMode.None);
        myMeshRenderer.enabled = false;
        myRigidBody.useGravity = false;
    }
    void OnEnable()
    {
        StartTimerBeforeFall = Time.time;
    }
    void Update()
    {
        if(FallAfterTimer == true && OnGround == false && Time.time >= StartTimerBeforeFall + TimeBeforeFall)
        {
            myMeshRenderer.enabled = true;
            myRigidBody.useGravity = true;
        }
        if(OnGround == true && GroundTimer >= 0f)
        {
                myRigidBody.useGravity = false;
                GroundTimer -= Time.deltaTime;
                if(GroundTimer <= 0f)
                {
                    Destroy(gameObject);
                }
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            myRigidBody.angularVelocity = Vector3.zero;
            myRigidBody.linearVelocity = Vector3.zero;
            myRigidBody.isKinematic = true;
            if(StaysOnTheGround == true)
            {
                OnGround = true;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}