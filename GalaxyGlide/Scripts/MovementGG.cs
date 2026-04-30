using UnityEngine;
using UnityEngine.InputSystem;

public class MovementGG : MonoBehaviour
{
    [SerializeField] InputAction Up;
    [SerializeField] float thrustForce = 0f;
    Rigidbody myRigidBody;
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
    }
    void OnEnable()
    {
        Up.Enable();
    }

    void FixedUpdate()
    {
        if(Up.IsPressed())
        {
            myRigidBody.AddRelativeForce(Vector3.up * thrustForce * Time.fixedDeltaTime);
        }
    }
}
