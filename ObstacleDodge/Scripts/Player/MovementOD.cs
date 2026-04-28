using UnityEngine;

public class MovementOD : MonoBehaviour
{
    public float strafeSpeed = 0f;
    Rigidbody myRigidBody;
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