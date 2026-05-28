using UnityEngine;

public class ScoreCollectible : MonoBehaviour
{
    [SerializeField] int scoreAmount = 100;

    CollectibleAudio collectibleAudio;

    void Start()
    {
        collectibleAudio = GetComponent<CollectibleAudio>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (collectibleAudio != null)
            {
                collectibleAudio.PlayCollectSound();
            }

            ScoringOD.instance.AddScore(scoreAmount);
            Destroy(gameObject);
        }
    }
}