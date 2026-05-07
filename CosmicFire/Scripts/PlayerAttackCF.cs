using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackCF : MonoBehaviour
{
    bool isFiring = false;

    void Update()
    {
        ProcessFiring();
    }
    public void OnAttack(InputValue value)
    {
        isFiring = value.isPressed;
    }

    void ProcessFiring()
    {
        if(isFiring)
        {
            Debug.Log("Fire");
        }
    }
}
