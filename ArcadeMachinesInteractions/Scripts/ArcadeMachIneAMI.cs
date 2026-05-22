using UnityEngine;

public class ArcadeMachineAMI : MonoBehaviour
{
    [Header("Scene Settings")]
    [SerializeField] string gameScene = "GameScene";
    [SerializeField] float loadDelay = 2f;

    [Header("Player Detection")]
    [SerializeField] bool playerNear = false;

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
            playerNear = false;
    }

    void Update()
    {
        if (playerNear && Input.GetKeyDown(KeyCode.E) && !activated)
            StartArcadeMachine();
    }

    void StartArcadeMachine()
    {
        activated = true;
        Debug.Log("Loading game...");
        SceneTransitionManagerAMI.instance.LoadScene(gameScene, loadDelay);
    }
}