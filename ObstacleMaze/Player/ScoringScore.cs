using UnityEngine;

public class ScoringScore : MonoBehaviour
{
    public float ScoreCounter = 0f;
    Vector3 lastPosition;
    public float MoveCheck = 0f;
    public float ScoreMultiplier = 1f;
    [SerializeField] float ScorePenalty = 0f;

    void Update()
    {
        ScoreCounter += MoveCheck * ScoreMultiplier;
    }
}
