using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Collections;

public class ArcadeMachineAMI : MonoBehaviour
{
    [SerializeField] string gameScene = "GameScene";
    [SerializeField] float loadDelay = 2f;
    [SerializeField] int gameCost = 10;

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
                if (CanAffordGame())
                {
                    StartCoroutine(LoadGameScene());
                }
                else
                {
                    Debug.Log(" Not enough credits! Need " + gameCost + ", have " + PlayerCreditsAMI.instance.GetCredits());
                }
            }
        }
    }

    bool CanAffordGame()
    {
        if (PlayerCreditsAMI.instance == null)
        {
            Debug.LogError("PlayerCreditsAMI not found!");
            return false;
        }

        int currentCredits = PlayerCreditsAMI.instance.GetCredits();
        
        if (currentCredits >= gameCost)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    IEnumerator LoadGameScene()
    {
        isLoading = true;
        
        ArcadeMachineUI uiScript = GetComponent<ArcadeMachineUI>();
        if (uiScript != null)
        {
            uiScript.HideInteractionText();
        }
        
        PlayerCreditsAMI.instance.SpendCredits(gameCost);
        Debug.Log("Game cost: -" + gameCost + " credits");
        
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