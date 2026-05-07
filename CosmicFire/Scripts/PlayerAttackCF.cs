using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackCF : MonoBehaviour
{
    [SerializeField] GameObject[] firingParticles;
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
        foreach (GameObject firingParticle in firingParticles)
        {
            ParticleSystem.EmissionModule emmision = firingParticle.GetComponent<ParticleSystem>().emission;
            emmision.enabled = isFiring;
        }
    }
}
