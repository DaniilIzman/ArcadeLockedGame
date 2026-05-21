using UnityEngine;
using UnityEngine.SceneManagement;

public class ArcadeMachineAMI : MonoBehaviour
{
    [SerializeField] string gameScene = "GameScene";
    [SerializeField] float loadDelay = 2f;
    
    bool playerNear = false;
    bool activated = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;
            Debug.Log("Press E to play");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;
        }
    }

    void Update()
    {
        if (playerNear && Input.GetKeyDown(KeyCode.E) && !activated)
        {
            activated = true;
            Debug.Log("Loading game...");
            Invoke("LoadGame", loadDelay);
        }
    }

    void LoadGame()
    {
        SceneManager.LoadScene(gameScene);
    }
}