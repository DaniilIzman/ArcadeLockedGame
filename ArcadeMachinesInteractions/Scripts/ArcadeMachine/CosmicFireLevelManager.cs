using UnityEngine;

public class CosmicFireLevelManager : MonoBehaviour
{
    [System.Serializable]
    public class Level
    {
        public string levelName;
        public string sceneName;
    }

    [Header("Cosmic Fire Levels")]
    [SerializeField] Level[] levels;

    public Level[] GetLevels()
    {
        return levels;
    }
}