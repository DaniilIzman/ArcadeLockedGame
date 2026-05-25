using UnityEngine;

public class PlayerPositionManager : MonoBehaviour
{
    public static PlayerPositionManager instance;

    Vector3 savedPosition;
    Quaternion savedRotation;
    bool hasData = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SavePositionAndRotation(Vector3 position, Quaternion rotation)
    {
        savedPosition = position;
        savedRotation = rotation;
        hasData = true;
    }

    public Vector3 GetSavedPosition()
    {
        return savedPosition;
    }

    public Quaternion GetSavedRotation()
    {
        return savedRotation;
    }

    public bool HasData()
    {
        return hasData;
    }

    public void ClearData()
    {
        hasData = false;
    }
}