using UnityEngine;

public class ScoreSystemGG : MonoBehaviour
{
    int currentScore = 0;

    void Start()
    {
        currentScore = 0;
        Debug.Log("Score: 0");
    }

    public int GetCurrentScore()
    {
        return currentScore;
    }

    public void AddScore(int points)
    {
        currentScore += points;
        Debug.Log("Score: " + currentScore + " (+" + points + ")");
    }

    public void ResetScore()
    {
        currentScore = 0;
    }
}