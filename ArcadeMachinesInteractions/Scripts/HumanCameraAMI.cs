using UnityEngine;

public class HumanCameraAMI : MonoBehaviour
{
    public float sensitivityX;
    public float sensitivityY;

    public Transform direction;

    float rotationX;
    float rotationY;

    void Start()
    {
        Cursor.lockState  = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * sensitivityX;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * sensitivityY;

        rotationY += mouseX;
        rotationX -= mouseY;
    }
}
