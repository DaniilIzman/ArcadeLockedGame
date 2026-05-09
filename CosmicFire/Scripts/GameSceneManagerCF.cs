using UnityEngine;
using UnityEngine.SceneManagement;
public class GameSceneManagerCF : MonoBehaviour
{

    public void ReloadLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
        Debug.Log("Reload scene");
    }
}
