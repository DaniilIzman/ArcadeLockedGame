using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackCF : MonoBehaviour
{
    [SerializeField] GameObject[] firingParticles;
    [SerializeField] RectTransform crosshair;
    bool isFiring = false;

    void Start() 
    {
        Cursor.visible = false;
    }

    void Update()
    {
        MoveCrosshair();
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

    void MoveCrosshair()
    {
        crosshair.position = Input.mousePosition;
    }
}
