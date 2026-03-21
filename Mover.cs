using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] float moveSpeed = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PrintInConsole();
    }

    // Player movement
    void MovePlayer()
    {
        float xValue = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        float yValue = 0f;
        float zValue = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        transform.Translate(xValue, yValue, zValue);
    }
    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    // Information in console
    void PrintInConsole()
    {
        Debug.Log("Welcome!");
        Debug.Log("Use the WASD keys or the arrow keys to control your character’s movement");
        Debug.Log("Don't bump into objects.");
    }
}
