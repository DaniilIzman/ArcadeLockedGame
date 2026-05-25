using UnityEngine;

public class LevelManagerAMI : MonoBehaviour
{
    [System.Serializable]
    public class Level
    {
        public string levelName;
        public string sceneName;
    }

    [Header("Levels")]
    [SerializeField] Level[] levels;

    public Level[] GetLevels()
    {
        return levels;
    }

    public Level GetLevel(int index)
    {
        if (index >= 0 && index < levels.Length)
        {
            return levels[index];
        }

        return null;
    }
}