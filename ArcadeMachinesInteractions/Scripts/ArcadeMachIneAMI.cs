using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Collections;

public class ArcadeMachineAMI : MonoBehaviour
{
    [SerializeField] string gameScene = "GameScene";
    [SerializeField] float loadDelay = 2f;

    bool playerNear = false;
    bool isLoading = false;
    Transform playerTransform;
    HumanMovementAMI playerMovement;
    Rigidbody playerRigidbody;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;
            playerTransform = other.transform;
            playerMovement = other.GetComponent<HumanMovementAMI>();
            playerRigidbody = other.GetComponent<Rigidbody>();
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
        if (playerNear && !isLoading)
        {
            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                StartCoroutine(LoadGameScene());
            }
        }
    }

    IEnumerator LoadGameScene()
    {
        isLoading = true;
        
        PlayerPositionManager.instance.SavePosition(playerTransform.position);
        playerMovement.enabled = false;
        playerRigidbody.linearVelocity = Vector3.zero;
        playerRigidbody.constraints = RigidbodyConstraints.FreezeAll;
        
        yield return new WaitForSeconds(loadDelay);
        
        SceneManager.LoadScene(gameScene);
    }
}