using UnityEngine;
using System.Collections;

public class FreezeCollectible : MonoBehaviour
{
    [SerializeField] private float freezeTimer = 2f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MovementOD playerMovement = other.GetComponent<MovementOD>();
            
            if (playerMovement != null)
            {
                StartCoroutine(FreezeRoutine(playerMovement));
            }
        }
    }

    private IEnumerator FreezeRoutine(MovementOD playerMovement)
    {
        Collider col = GetComponent<Collider>();
        if (col != null) col.enabled = false;

        MeshRenderer mesh = GetComponent<MeshRenderer>();
        if (mesh != null) mesh.enabled = false;

        playerMovement.canMove = false;
        
        Rigidbody rb = playerMovement.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f); 
        }

        yield return new WaitForSeconds(freezeTimer);

        playerMovement.canMove = true;

        Destroy(gameObject);
    }
}