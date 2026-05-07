using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackCF : MonoBehaviour
{
    [SerializeField] float targetDistance = 0f;
    [SerializeField] GameObject[] firingParticles;
    [SerializeField] RectTransform crosshair;
    [SerializeField] Transform targetPoint;

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

    void MoveTargetPoint()
    {
        Vector3 targetPointPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, targetDistance);
        targetPoint.position = Camera.main.ScreenToWorldPoint(targetPointPosition);
    }
}
