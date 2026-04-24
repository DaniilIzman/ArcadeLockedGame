using UnityEngine;

public class ScoringScore : MonoBehaviour
{
    public float ScoreCounter = 0f;
    public float MoveCheck = 0f;
    public float ScoreMultiplier = 1f;
    void Update()
    {
        ScoreCounter += MoveCheck * ScoreMultiplier;
    }
}
