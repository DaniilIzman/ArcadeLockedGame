using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameSceneManagerCF : MonoBehaviour
{

    [SerializeField] float reloadLevelDelay = 0f;
    public void ReloadLevel()
    {
        StartCoroutine(ReloadLevelRoutine());
    }

    IEnumerator ReloadLevelRoutine()
    {
        yield return new WaitForSeconds(reloadLevelDelay);
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
}
