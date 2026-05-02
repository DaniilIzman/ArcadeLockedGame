using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
public class CollisionDetectorGG : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Spawn":
                Debug.Log("Spawnpoint");
                break;
            case "Obstacle":
                Debug.Log("Obstacle");
                break;
            case "Finish":
                LoadNextLevelScene();
                break;
            default:
                EffectsAfterCrash();
                break;
        }
    }

    void EffectsAfterCrash()
    {
        Invoke("ReloadLevelScene", 3f);
    }

    void ReloadLevelScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    void LoadNextLevelScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;
        if(nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }
        SceneManager.LoadScene(nextScene);
    }
}
