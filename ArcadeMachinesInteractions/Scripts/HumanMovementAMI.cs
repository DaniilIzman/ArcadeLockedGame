using UnityEngine;

public class HumanMovementAMI : MonoBehaviour
{
    public float moveSpeed = 5f;

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        transform.Translate(move * moveSpeed * Time.deltaTime, Space.World);
    }
}