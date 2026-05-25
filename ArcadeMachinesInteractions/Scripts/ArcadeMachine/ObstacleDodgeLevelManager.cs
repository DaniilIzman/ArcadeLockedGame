using UnityEngine;

public class ObstacleDodgeLevelManager : MonoBehaviour
{
    [System.Serializable]
    public class Level
    {
        public string levelName;
        public string sceneName;
    }

    [Header("Obstacle Dodge Levels")]
    [SerializeField] Level[] levels;

    public Level[] GetLevels()
    {
        return levels;
    }
}