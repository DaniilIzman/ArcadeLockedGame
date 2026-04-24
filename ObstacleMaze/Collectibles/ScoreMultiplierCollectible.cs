using UnityEngine;
using System.Collections;
public class ScoreMultiplierCollectible : MonoBehaviour
{
    [SerializeField] float MyScoreMultiplier = 0f;
    [SerializeField] bool ScoreTimerActivate;
    [SerializeField] float ScoreMultiplierTimer = 0f;
    ScoringScore Score;

    void Awake()
    {
        Score = FindAnyObjectByType<ScoringScore>();
    }
    IEnumerator ResetScoreMultiplier(float originalMultiplier, float time)
    {
        yield return new WaitForSeconds(time);
        Score.ScoreMultiplier = originalMultiplier;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            float originalMultiplier = Score.ScoreMultiplier;
            Score.ScoreMultiplier = MyScoreMultiplier;
            if(ScoreTimerActivate == true && ScoreMultiplierTimer > 0f && MyScoreMultiplier > 0f)
            {
                Debug.Log("Score multiplier activated!");
                Score.StartCoroutine(ResetScoreMultiplier(originalMultiplier, ScoreMultiplierTimer));
            }
            Destroy(gameObject);
            
        }
    }
}
