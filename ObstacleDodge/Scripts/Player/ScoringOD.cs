using UnityEngine;

public class ScoringOD : MonoBehaviour
{
    public static ScoringOD instance;

    int currentScore = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        ResetScore();
    }

    public void AddScore(int points)
    {
        currentScore += points;
        Debug.Log($"Score: +{points}. Total: {currentScore}");
    }

    public int GetCurrentScore()
    {
        return currentScore;
    }

    public void ResetScore()
    {
        currentScore = 0;
    }
}