using UnityEngine;
using TMPro;

public class ArcadeMachineUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI interactionText;
    [SerializeField] string playText = "Press E to play";
    [SerializeField] string insufficientText = "Insufficient Credits";

    HumanMovementAMI playerMovement;
    ArcadeMachineAMI arcadeMachine;

    void Start()
    {
        interactionText.enabled = false;
        arcadeMachine = GetComponent<ArcadeMachineAMI>();
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
        }
        else
        {
            interactionText.text = insufficientText + " (Need: " + gameCost + ")";
            interactionText.color = Color.red;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerMovement = other.GetComponent<HumanMovementAMI>();
            arcadeMachine = GetComponent<ArcadeMachineAMI>();
            interactionText.enabled = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactionText.enabled = false;
            playerMovement = null;
        }
    }

    public void HideInteractionText()
    {
        this.enabled = false;
        interactionText.enabled = false;
    }
}