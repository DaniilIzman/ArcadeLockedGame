using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] float moveSpeed = 0f;
    Rigidbody myRigidBody;
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
        PrintInConsole();
    }

    void MovePlayer()
    {
        float xValue = Input.GetAxis("Horizontal") * Time.fixedDeltaTime * moveSpeed;
        float yValue = 0f;
        float zValue = Input.GetAxis("Vertical") * Time.fixedDeltaTime * moveSpeed;
        Vector3 movement = new Vector3(xValue, yValue, zValue);
        myRigidBody.MovePosition(myRigidBody.position + movement);
    }
    void FixedUpdate()
    {
        MovePlayer();
    }

    void PrintInConsole()
    {
        Debug.Log("Welcome!");
        Debug.Log("Use the WASD keys or the arrow keys to control your character’s movement");
        Debug.Log("Don't bump into objects.");
    }
}
