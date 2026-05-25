using UnityEngine;
using UnityEngine.InputSystem;

public class QuitTheGame : MonoBehaviour
{
    void Update()
    {
        if(Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Debug.Log("Escape is pressed!");
            Application.Quit();
        }
    }

}
