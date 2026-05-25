using UnityEngine;
using TMPro;

public class ArcadeMachineUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI interactionText;
    [SerializeField] string playText = "Press E to play";
    [SerializeField] string insufficientText = "Insufficient Credits";

    HumanMovementAMI playerMovement;
    ArcadeMachineAMI arcadeMachine;
    ArcadeMachineAudio arcadeAudio;
    bool hasShownInsufficientAlert = false;

    void Start()
    {
        interactionText.enabled = false;
        arcadeMachine = GetComponent<ArcadeMachineAMI>();
        arcadeAudio = GetComponent<ArcadeMachineAudio>();
    }

    void Update()
    {
        if (playerMovement != null && arcadeMachine != null)
        {
            if (playerMovement.GetGroundContactCount() > 0)
            {
                UpdateInteractionText();
                interactionText.enabled = true;
            }
            else
            {
                interactionText.enabled = false;
                hasShownInsufficientAlert = false;
            }
        }
    }

    void UpdateInteractionText()
    {
        int gameCost = arcadeMachine.GetGameCost();
        int currentCredits = PlayerCreditsAMI.instance.GetCredits();
        
        if (currentCredits >= gameCost)
        {
            interactionText.text = playText + " (Cost: " + gameCost + ")";
            interactionText.color = Color.white;
            arcadeMachine.SetInteractable(true);
            hasShownInsufficientAlert = false;
        }
        else
        {
            interactionText.text = insufficientText + " (Need: " + gameCost + ")";
            interactionText.color = Color.red;
            arcadeMachine.SetInteractable(false);
            
            if (!hasShownInsufficientAlert)
            {
                if (arcadeAudio != null)
                {
                    arcadeAudio.PlayInsufficientCreditsSound();
                }
                hasShownInsufficientAlert = true;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerMovement = other.GetComponent<HumanMovementAMI>();
            arcadeMachine = GetComponent<ArcadeMachineAMI>();
            arcadeAudio = GetComponent<ArcadeMachineAudio>();
            
            int gameCost = arcadeMachine.GetGameCost();
            int currentCredits = PlayerCreditsAMI.instance.GetCredits();
            
            if (currentCredits >= gameCost)
            {
                if (arcadeAudio != null)
                {
                    arcadeAudio.PlayTextAppearSound();
                }
            }
            
            interactionText.enabled = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactionText.enabled = false;
            playerMovement = null;
            hasShownInsufficientAlert = false;
        }
    }

    public void HideInteractionText()
    {
        this.enabled = false;
        interactionText.enabled = false;
    }
}