using UnityEngine;
using UnityEngine.InputSystem;

public class MovementGG : MonoBehaviour
{
    [SerializeField] InputAction Up;
    [SerializeField] InputAction Rotation;
    [SerializeField] float thrustForce = 0f;
    Rigidbody myRigidBody;
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
    }
    void OnEnable()
    {
        Up.Enable();
        Rotation.Enable();
    }

    void FixedUpdate()
    {
        ThrustController();
        RotationController();
    }

    private void ThrustController()
    {
        if (Up.IsPressed())
        {
            myRigidBody.AddRelativeForce(Vector3.up * thrustForce * Time.fixedDeltaTime);
        }
    }

    private void RotationController()
    {
        float RotationInput = Rotation.ReadValue<float>();
        Debug.Log(RotationInput);
    }
}
