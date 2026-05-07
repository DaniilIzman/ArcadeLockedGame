using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackCF : MonoBehaviour
{

    public void OnAttack(InputValue value)
    {
        Debug.Log("Fire");
    }
}
