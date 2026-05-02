using UnityEngine;
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
                Debug.Log("Finish");
                break;
            default:
                reloadGGgame();
                break;
        }

        void reloadGGgame()
        {
            int currentScene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentScene);
        }
    }
}
