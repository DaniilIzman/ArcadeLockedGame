using UnityEngine;

public class PlayerPositionManager : MonoBehaviour
{
    public static PlayerPositionManager instance;

    private Vector3 savedPosition;
    private bool hasSavedPosition = false;

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

    public void SavePlayerPosition(Vector3 position)
    {
        savedPosition = position;
        hasSavedPosition = true;
        Debug.Log("Position saved: " + position);
    }

    public bool TryLoadPlayerPosition(out Vector3 position)
    {
        position = savedPosition;
        return hasSavedPosition;
    }

    public void ClearSavedPosition()
    {
        hasSavedPosition = false;
    }
}