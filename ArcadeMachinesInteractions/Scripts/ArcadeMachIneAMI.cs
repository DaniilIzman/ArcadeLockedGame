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
    HumanCameraAMI playerCamera;
    Rigidbody playerRigidbody;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;
            playerTransform = other.transform;
            playerMovement = other.GetComponent<HumanMovementAMI>();
            playerCamera = other.GetComponentInChildren<HumanCameraAMI>();
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
        if (playerNear && !isLoading && IsPlayerGrounded())
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
        
        FreezePlayer();
        
        yield return new WaitForSeconds(loadDelay);
        
        SceneManager.LoadScene(gameScene);
    }

    void FreezePlayer()
    {
        PlayerPositionManager.instance.SavePositionAndRotation(playerTransform.position, playerTransform.rotation);
        
        playerMovement.enabled = false;
        playerCamera.enabled = false;
        playerRigidbody.linearVelocity = Vector3.zero;
        playerRigidbody.constraints = RigidbodyConstraints.FreezeAll;
    }

    bool IsPlayerGrounded()
    {
        if (playerMovement == null)
            return false;

        return playerMovement.GetGroundContactCount() > 0;
    }
}