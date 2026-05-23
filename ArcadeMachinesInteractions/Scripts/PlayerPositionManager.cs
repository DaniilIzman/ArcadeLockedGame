using UnityEngine;

public class PlayerPositionManager : MonoBehaviour
{
    public static PlayerPositionManager instance;

    Vector3 savedPosition;
    bool hasPosition = false;

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

    public void SavePosition(Vector3 position)
    {
        savedPosition = position;
        hasPosition = true;
    }

    public Vector3 GetSavedPosition()
    {
        return savedPosition;
    }

    public bool HasPosition()
    {
        return hasPosition;
    }

    public void ClearPosition()
    {
        hasPosition = false;
    }
}