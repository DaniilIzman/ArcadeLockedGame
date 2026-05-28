using UnityEngine;

public class HPCollectible : MonoBehaviour
{
    [SerializeField] int healthRestore = 1;

    HealthOD PlayerHealth;
    CollectibleAudio collectibleAudio;

    void Awake()
    {
        PlayerHealth = FindAnyObjectByType<HealthOD>();
    }

    void Start()
    {
        collectibleAudio = GetComponent<CollectibleAudio>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (collectibleAudio != null)
                collectibleAudio.PlayCollectSound();

            if (PlayerHealth != null)
            {
                PlayerHealth.bumpCounter -= healthRestore;
                PlayerHealth.bumpCounter = Mathf.Max(PlayerHealth.bumpCounter, 0);
            }

            Destroy(gameObject);
        }
    }
}