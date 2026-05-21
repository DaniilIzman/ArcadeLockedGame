using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackCF : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject[] firingParticles;
    [SerializeField] RectTransform crosshair;
    [SerializeField] Transform targetPoint;

    [Header("Settings")]
    [SerializeField] float targetDistance = 0f;

    bool isFiring = false;

    void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        MoveTargetPoint();
        MoveCrosshair();
        ProcessFiring();
        AimLasers();
    }

    public void OnAttack(InputValue value)
    {
        isFiring = value.isPressed;
    }

    void ProcessFiring()
    {
        foreach (GameObject firingParticle in firingParticles)
        {
            ParticleSystem.EmissionModule emission = firingParticle.GetComponent<ParticleSystem>().emission;

            emission.enabled = isFiring;
        }
    }

    void MoveCrosshair()
    {
        crosshair.position = Input.mousePosition;
    }

    void MoveTargetPoint()
    {
        Vector3 targetPointPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, targetDistance);

        targetPoint.position = Camera.main.ScreenToWorldPoint(targetPointPosition);
    }

    void AimLasers()
    {
        foreach (GameObject firingParticle in firingParticles)
        {
            Vector3 fireDirection = targetPoint.position - transform.position;

            Quaternion rotationToTarget = Quaternion.LookRotation(fireDirection);

            firingParticle.transform.rotation = rotationToTarget;
        }
    }
}