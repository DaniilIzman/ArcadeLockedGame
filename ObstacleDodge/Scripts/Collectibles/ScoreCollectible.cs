using UnityEngine;
using System.Collections;
public class ScoreMultiplierCollectible : MonoBehaviour
{
    [SerializeField] float MyScoreMultiplier = 0f;
    [SerializeField] bool ScoreTimerActivate;
    [SerializeField] float ScoreMultiplierTimer = 0f;
    ScoringOD PlayerScore;

    void Awake()
    {
        PlayerScore = FindAnyObjectByType<ScoringOD>();
    }
    IEnumerator ResetScoreMultiplier(float originalMultiplier, float time)
    {
        yield return new WaitForSeconds(time);
        PlayerScore.ScoreMultiplier = originalMultiplier;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            float originalMultiplier = PlayerScore.ScoreMultiplier;
            PlayerScore.ScoreMultiplier = MyScoreMultiplier;
            if(ScoreTimerActivate == true && ScoreMultiplierTimer > 0f && MyScoreMultiplier > 0f)
            {
                Debug.Log("Score multiplier activated!");
                PlayerScore.StartCoroutine(ResetScoreMultiplier(originalMultiplier, ScoreMultiplierTimer));
            }
            Destroy(gameObject);
            
        }
    }
}
