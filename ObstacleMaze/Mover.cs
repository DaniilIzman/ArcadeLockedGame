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
        float xValue = Input.GetAxis("Horizontal") * Time.fixedDeltaTime * moveSpeed;
        float yValue = 0f;
        float zValue = Input.GetAxis("Vertical") * Time.fixedDeltaTime * moveSpeed;
        Vector3 movement = new Vector3(xValue, yValue, zValue);
        myRigidBody.MovePosition(myRigidBody.position + movement);
    }
    public void MovePlayerTanky()
    {
        float moveInput = Input.GetAxis("Vertical") * moveSpeed * Time.fixedDeltaTime;
        float turnInput = Input.GetAxis("Horizontal") * rotationSpeed * Time.fixedDeltaTime;
        if(MoveWhileRotating == false)
        {
            if (Mathf.Abs(turnInput) > 0.01f)
            {
                    Quaternion rotation = Quaternion.Euler(0f, turnInput, 0f);
                    myRigidBody.MoveRotation(myRigidBody.rotation * rotation);
            }
            else
            {
                    Vector3 movement = transform.forward * moveInput;
                    myRigidBody.MovePosition(myRigidBody.position + movement);
            }
        }
        else
        {
            Vector3 movement = transform.forward * moveInput;
            myRigidBody.MovePosition(myRigidBody.position + movement);
            Quaternion rotation = Quaternion.Euler(0f, turnInput, 0f);
            myRigidBody.MoveRotation(myRigidBody.rotation * rotation);
        }

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
