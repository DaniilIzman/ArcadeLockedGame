using UnityEngine;

public class DropperObstacle : MonoBehaviour
{
    [SerializeField] bool FallAfterTimer;
    [SerializeField] float TimeBeforeFall = 3f;
    [SerializeField] bool StaysOnTheGround;
    [SerializeField] float GroundTimer = 0f;
    [SerializeField] bool FallOnTrigger;
    MeshRenderer myMeshRenderer;
    bool OnGround = false;
    
    Rigidbody myRigidBody;
    void Start()
    {
        myMeshRenderer = GetComponent<MeshRenderer>();
        myRigidBody = GetComponent<Rigidbody>();

        myMeshRenderer.enabled = false;
        myRigidBody.useGravity = false;
    }

    void Update()
    {
        if(Time.time > TimeBeforeFall)
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