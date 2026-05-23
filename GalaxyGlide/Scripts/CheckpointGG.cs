using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] int checkpointNumber = 1;
    [SerializeField] int checkpointPoints = 100;
    bool alreadySaved = false;

    void Start()
    {
        Debug.Log("Checkpoint " + checkpointNumber + " initialized");
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name);

        if (collision.gameObject.GetComponent<MovementGG>() != null && !alreadySaved)
        {
            alreadySaved = true;
            
            Vector3 checkpointPos = transform.position;

            CheckpointManagerGG.instance.SaveCheckpointPosition(checkpointPos, Quaternion.identity);
            
            ScoreSystemGG score = collision.gameObject.GetComponent<ScoreSystemGG>();
            if (score != null)
            {
                score.AddScore(checkpointPoints);
            }
            
            Debug.Log("CHECKPOINT " + checkpointNumber + " SAVED!");
            Debug.Log("Position: " + checkpointPos);
        }
    }
}