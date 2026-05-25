using UnityEngine;

public class CheckpointManagerGG : MonoBehaviour
{
    public static CheckpointManagerGG instance;

    Vector3 lastCheckpointPosition;
    Quaternion lastCheckpointRotation;
    bool checkpointReached = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Debug.Log("CheckpointManager initialized");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveCheckpointPosition(Vector3 position, Quaternion rotation)
    {
        lastCheckpointPosition = position;
        lastCheckpointRotation = rotation;
        checkpointReached = true;
        Debug.Log("Checkpoint saved at position: " + position);
    }

    public Vector3 GetLastCheckpointPosition()
    {
        return lastCheckpointPosition;
    }

    public Quaternion GetLastCheckpointRotation()
    {
        return lastCheckpointRotation;
    }

    public bool HasCheckpointBeenReached()
    {
        return checkpointReached;
    }

    public void ResetCheckpoints()
    {
        lastCheckpointPosition = Vector3.zero;
        lastCheckpointRotation = Quaternion.identity;
        checkpointReached = false;
        Debug.Log("Checkpoints reset");
    }
}