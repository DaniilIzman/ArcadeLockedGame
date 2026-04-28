using UnityEngine;
public class HPCollectible : MonoBehaviour
{
    [SerializeField] int AdditionalBumps = 0;
    HealthOD PlayerHealth;
    void Awake()
    {
        PlayerHealth = FindAnyObjectByType<HealthOD>();
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(PlayerHealth.bumpCounter > 0)
            {
                PlayerHealth.bumpCounter -= AdditionalBumps;
                PlayerHealth.bumpCounter = Mathf.Max(PlayerHealth.bumpCounter, 0);
                Destroy(gameObject);
            }
        }
    }
}