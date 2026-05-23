using UnityEngine;

public class CheckpointManagerGG : MonoBehaviour
{
    public static CheckpointManagerGG instance;

    Vector3 lastCheckpointPosition;
    Quaternion lastCheckpointRotation;

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
        Debug.Log("CheckpointManager saved position: " + position);
    }

    public Vector3 GetLastCheckpointPosition()
    {
        Debug.Log("Retrieving checkpoint position: " + lastCheckpointPosition);
        return lastCheckpointPosition;
    }

    public Quaternion GetLastCheckpointRotation()
    {
        return lastCheckpointRotation;
    }
}