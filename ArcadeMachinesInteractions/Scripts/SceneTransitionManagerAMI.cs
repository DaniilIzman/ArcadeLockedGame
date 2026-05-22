using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransitionManagerAMI : MonoBehaviour
{
    public static SceneTransitionManagerAMI instance;

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
        StartCoroutine(LoadSceneByNameCoroutine(sceneName, delay));
    }

    public void ReloadCurrentScene(float delay = 0f)
    {
        StartCoroutine(LoadSceneByIndexCoroutine(SceneManager.GetActiveScene().buildIndex, delay));
    }

    public void LoadNextScene(float delay = 0f)
    {
        int next = SceneManager.GetActiveScene().buildIndex + 1;
        if (next >= SceneManager.sceneCountInBuildSettings)
            next = 0;

        StartCoroutine(LoadSceneByIndexCoroutine(next, delay));
    }

    IEnumerator LoadSceneByNameCoroutine(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator LoadSceneByIndexCoroutine(int index, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(index);
    }
}