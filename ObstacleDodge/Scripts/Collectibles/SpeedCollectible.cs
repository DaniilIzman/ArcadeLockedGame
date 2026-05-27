using UnityEngine;
using System.Collections;

public class SpeedCollectible : MonoBehaviour
{
    [SerializeField] private float speedMultiplier = 1.5f;
    [SerializeField] private bool speedTimerActivate = true;
    [SerializeField] private float speedTimer = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MovementOD playerMovement = other.GetComponent<MovementOD>();
            
            if (playerMovement != null)
            {
                StartCoroutine(SpeedRoutine(playerMovement));
            }
        }
    }

    private IEnumerator SpeedRoutine(MovementOD playerMovement)
    {
        Collider col = GetComponent<Collider>();
        if (col != null) col.enabled = false;

        MeshRenderer mesh = GetComponent<MeshRenderer>();
        if (mesh != null) mesh.enabled = false;

        float originalMoveSpeed = playerMovement.moveSpeed;
        float originalRotationSpeed = playerMovement.rotationSpeed;

        playerMovement.moveSpeed *= speedMultiplier;
        playerMovement.rotationSpeed *= speedMultiplier;

        if (speedTimerActivate && speedTimer > 0f && speedMultiplier > 0f)
        {
            yield return new WaitForSeconds(speedTimer);
            playerMovement.moveSpeed = originalMoveSpeed;
            playerMovement.rotationSpeed = originalRotationSpeed;
        }

        Destroy(gameObject);
    }
}