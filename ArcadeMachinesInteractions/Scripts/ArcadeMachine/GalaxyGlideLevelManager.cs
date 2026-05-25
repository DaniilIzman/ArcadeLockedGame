using UnityEngine;

public class GalaxyGlideLevelManager : MonoBehaviour
{
    [System.Serializable]
    public class Level
    {
        public string levelName;
        public string sceneName;
    }

    [Header("Galaxy Glide Levels")]
    [SerializeField] Level[] levels;

    public Level[] GetLevels()
    {
        return levels;
    }
}