using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransitionManagerAMI : MonoBehaviour
{
    public static SceneTransitionManager instance;

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

    public void LoadScene(string sceneName, float delay = 0f)
    {
        StartCoroutine(LoadSceneCoroutine(sceneName, delay));
    }

    public void ReloadCurrentScene(float delay = 0f)
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(LoadSceneCoroutine(currentScene.ToString(), delay));
    }

    public void LoadNextScene(float delay = 0f)
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;

        if (nextScene >= SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }

        StartCoroutine(LoadSceneCoroutine(nextScene.ToString(), delay));
    }

    IEnumerator LoadSceneCoroutine(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}