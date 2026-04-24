using UnityEngine;

public class ScoringScore : MonoBehaviour
{
    public float ScoreCounter = 0f;
    Vector3 lastPosition;
    public float MoveCheck = 0f;
    public float ScoreMultiplier = 1f;
    [SerializeField] float ScorePenalty = 0f;
    void Start()
    {
        lastPosition = transform.position;
    }

    void Update()
    {
        MoveCheck = transform.position.z - lastPosition.z;
        
        if(MoveCheck > 0f)
        {
            ScoreCounter += MoveCheck * ScoreMultiplier;
            Debug.Log("Moving forward. Score: " + ScoreCounter);
        }

        else if(MoveCheck < 0)
        {
            ScoreCounter += MoveCheck * ScorePenalty * ScoreMultiplier;
            Debug.Log("Moving back. Score penalty: " + ScoreCounter);
        }

        else
        {
            Debug.Log("Standing still. Move!");
        }
        lastPosition = transform.position;

    }
}
