using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackCF : MonoBehaviour
{
    [SerializeField] GameObject firingParticle;
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
        ParticleSystem.EmissionModule emmision = firingParticle.GetComponent<ParticleSystem>().emission;
        emmision.enabled = isFiring;
    }
}
