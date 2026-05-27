using UnityEngine;

public class ScoringOD : MonoBehaviour
{
    [Header("Score Settings")]
    public float ScoreMultiplier = 1f;
    
    private int _currentScore;

    public int CurrentScore
    {
        get { return _currentScore; }
    }

    void Start()
    {
        ResetScore();
    }

    public void AddScore(int basePoints)
    {
        int finalPoints = Mathf.RoundToInt(basePoints * ScoreMultiplier);
        _currentScore += finalPoints;

        Debug.Log($"Score Added: +{finalPoints}. Total: {_currentScore}");
    }

    public void ResetScore()
    {
        _currentScore = 0;
    }
}