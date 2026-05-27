using UnityEngine;

public class ScoringOD : MonoBehaviour
{
    [Header("Score Settings")]
    public float ScoreMultiplier = 1f;
    
    public int CurrentScore = 0;

    void Start()
    {
        ResetScore();
    }

    public void AddScore(int basePoints)
    {
        int finalPoints = Mathf.RoundToInt(basePoints * ScoreMultiplier);
        CurrentScore += finalPoints;

        Debug.Log($"Score Added: +{finalPoints}. Total: {CurrentScore}");
    }

    public void ResetScore()
    {
        CurrentScore = 0;
    }
}