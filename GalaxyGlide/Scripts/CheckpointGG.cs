using UnityEngine;

public class CheckpointGG : MonoBehaviour
{
    [SerializeField] int checkpointNumber = 1;
    bool alreadySaved = false;

    void Start()
    {
        Debug.Log("Checkpoint" + checkpointNumber + " initialized");
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name);

        if (collision.gameObject.GetComponent<MovementGG>() != null && !alreadySaved)
        {
            alreadySaved = true;
            
            Vector3 checkpointPos = transform.position;

            CheckpointManagerGG.instance.SaveCheckpointPosition(checkpointPos, Quaternion.identity);
            
            Debug.Log("CHECKPOINT" + checkpointNumber + " SAVED!");
            Debug.Log("Position: " + checkpointPos);
        }
    }
}