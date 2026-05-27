using UnityEngine;

public class ScoreCollectible : MonoBehaviour
{
    [SerializeField] int scoreAmount = 100;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ScoringOD.instance.AddScore(scoreAmount);
            Destroy(gameObject);
        }
    }
}