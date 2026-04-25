using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] bool TankyMovement;
    public float moveSpeed = 0f;
    [SerializeField] bool MoveWhileRotating;
    public float rotationSpeed = 0f;
    public bool canMove = true;
    Rigidbody myRigidBody;
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
        PrintInConsole();
    }

    public void MovePlayer()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(x, 0f, z).normalized;

        myRigidBody.linearVelocity = direction * moveSpeed;
    }
    public void MovePlayerTanky()
    {
        float moveInput = Input.GetAxis("Vertical");
        float turnInput = Input.GetAxis("Horizontal");

        Vector3 movement = transform.forward * moveInput * moveSpeed;
        myRigidBody.linearVelocity = movement;
        
        float rotationAmount = turnInput * rotationSpeed * Time.fixedDeltaTime;
        Quaternion rotation = Quaternion.Euler(0f, rotationAmount, 0f);
        myRigidBody.MoveRotation(myRigidBody.rotation * rotation);
    }
    void FixedUpdate()
    {
        if(canMove == true)
        {
            if(TankyMovement == true)
            {
                MovePlayerTanky();  
            }
            else
            {
                MovePlayer();  
            }
        }
    }

    void PrintInConsole()
    {
        Debug.Log("Welcome!");
        Debug.Log("Use the WASD keys or the arrow keys to control your character’s movement");
        Debug.Log("Don't bump into objects.");
    }
}
