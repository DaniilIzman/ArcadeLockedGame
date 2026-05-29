using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Collections;

public class ArcadeMachineAMI : MonoBehaviour
{
    [Header("Machine Settings")]
    [SerializeField] string machineName = "Game_1";
    [SerializeField] string gameScene   = "GameScene";
    [SerializeField] float  loadDelay  = 2f;
    [SerializeField] int    gameCost   = 10;

    bool playerNear      = false;
    bool isLoading       = false;
    bool isInteractable  = true;
    Transform playerTransform;
    HumanMovementAMI playerMovement;
    HumanCameraAMI   playerCamera;
    Rigidbody        playerRigidbody;
    ArcadeMachineAudio arcadeAudio;

    void Start()
    {
        arcadeAudio = GetComponent<ArcadeMachineAudio>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear      = true;
            playerTransform = other.transform;
            playerMovement  = other.GetComponent<HumanMovementAMI>();
            playerCamera    = other.GetComponentInChildren<HumanCameraAMI>();
            playerRigidbody = other.GetComponent<Rigidbody>();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerNear = false;
    }

    void Update()
    {
        if (playerNear && !isLoading && isInteractable)
        {
            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                if (CanAffordGame())
                {
                    if (arcadeAudio != null)
                        arcadeAudio.PlayConfirmSound();

                    StartCoroutine(LoadGameScene());
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
            return true;
        else
            return false;
    }

    IEnumerator LoadGameScene()
    {
        isLoading = true;

        ArcadeMachineUI uiScript = GetComponent<ArcadeMachineUI>();
        if (uiScript != null)
            uiScript.HideInteractionText();

        PlayerCreditsAMI.instance.SpendCredits(gameCost);
        Debug.Log(machineName + " - Game cost: -" + gameCost + " credits");

        FreezePlayer();

        yield return new WaitForSeconds(loadDelay);

        SceneManager.LoadScene(gameScene);
    }

    void FreezePlayer()
    {
        PlayerPositionManager.instance.SavePositionAndRotation(playerTransform.position, playerTransform.rotation);

        playerMovement.enabled  = false;
        playerCamera.enabled    = false;
        playerRigidbody.linearVelocity  = Vector3.zero;
        playerRigidbody.constraints     = RigidbodyConstraints.FreezeAll;
    }

    public void SetInteractable(bool value)
    {
        isInteractable = value;
    }

    public int GetGameCost()
    {
        return gameCost;
    }

    public string GetMachineName()
    {
        return machineName;
    }
}