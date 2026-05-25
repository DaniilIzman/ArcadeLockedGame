using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ArcadeGameMenuUI : MonoBehaviour
{
    [Header("Level Manager")]
    [SerializeField] LevelManagerAMI levelManager;

    [Header("UI References")]
    [SerializeField] Transform levelButtonContainer;
    [SerializeField] GameObject levelButtonPrefab;

    void Start()
    {
        PopulateLevelButtons();
    }

    void PopulateLevelButtons()
    {
        if (levelManager == null)
        {
            Debug.LogError("LevelManagerAMI not assigned!");
            return;
        }

        LevelManagerAMI.Level[] levels = levelManager.GetLevels();

        for (int i = 0; i < levels.Length; i++)
        {
            int levelIndex = i;
            LevelManagerAMI.Level level = levels[i];

            GameObject buttonObj = Instantiate(levelButtonPrefab, levelButtonContainer);

            TextMeshProUGUI label = buttonObj.GetComponentInChildren<TextMeshProUGUI>();
            if (label != null)
            {
                label.text = level.levelName;
            }

            Button button = buttonObj.GetComponent<Button>();
            if (button != null)
            {
                LevelButtonHandlerUI handler = buttonObj.AddComponent<LevelButtonHandlerUI>();
                handler.Setup(levelIndex, this);
                button.onClick.AddListener(handler.OnClick);
            }
        }
    }

    public void LoadLevel(int index)
    {
        LevelManagerAMI.Level level = levelManager.GetLevel(index);

        if (level != null)
        {
            SceneManager.LoadScene(level.sceneName);
        }
    }
}