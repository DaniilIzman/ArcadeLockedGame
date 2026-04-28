using UnityEngine;

public class MovementOD : MonoBehaviour
{
    [SerializeField] float strafeSpeed = 0f;
    Rigidbody myRigidBody;
    public float moveSpeed = 0f;
    public bool canMove = true;
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float xValue = Input.GetAxis("Horizontal");
        myRigidBody.linearVelocity = new Vector3(xValue * strafeSpeed, myRigidBody.linearVelocity.y, myRigidBody.linearVelocity.z);
    }
}