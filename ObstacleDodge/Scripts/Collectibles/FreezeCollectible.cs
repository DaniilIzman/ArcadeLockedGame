using UnityEngine;
using System.Collections;
public class FreezeCollectible : MonoBehaviour
{
    [SerializeField] float FreezeTimer = 0f;
    HealthOD PlayerHealth;
    MovementOD PlayerMovement;
    void Awake()
    {
        PlayerHealth = FindAnyObjectByType<HealthOD>();
        PlayerMovement = FindAnyObjectByType<MovementOD>();
    }
    IEnumerator UnfreezeAfter(float time)
    {
        yield return new WaitForSeconds(time);
        PlayerMovement.canMove = true;
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerMovement.canMove = false;
            PlayerMovement.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            PlayerMovement.StartCoroutine(UnfreezeAfter(FreezeTimer));
            Destroy(gameObject);
        }
    }
}