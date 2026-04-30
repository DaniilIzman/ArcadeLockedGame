using UnityEngine;
using UnityEngine.InputSystem;

public class MovementGG : MonoBehaviour
{
    [SerializeField] InputAction Up;

    void OnEnable()
    {
        Up.Enable();
    }

    void Update()
    {
        if(Up.IsPressed())
        {
            Debug.Log("Up button is pressed!");
        }
    }
}
