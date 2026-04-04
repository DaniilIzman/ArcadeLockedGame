using UnityEngine;

public class DropperObstacle : MonoBehaviour
{
    [SerializeField] float TimeBeforeFall = 3f;
    [SerializeField] bool StaysOnTheGround;
    [SerializeField] float GroundTimer = 0f;
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
            GroundTimer -= Time.deltaTime;
            if(GroundTimer <= 0f)
            {
                Destroy(gameObject);
            }
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
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