using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButtonUI : MonoBehaviour
{
    [SerializeField] string sceneName;

    Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        SceneManager.LoadScene(sceneName);
    }
}