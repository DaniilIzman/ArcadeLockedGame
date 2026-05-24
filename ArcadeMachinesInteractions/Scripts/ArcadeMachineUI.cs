using UnityEngine;
using TMPro;

public class ArcadeMachineUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI interactionText;
    [SerializeField] string displayText = "Press E to play";

    HumanMovementAMI playerMovement;
    ArcadeMachineAMI arcadeMachine;

    void Start()
    {
        interactionText.enabled = false;
    }

    void Update()
    {
        if (playerMovement != null)
        {
            if (playerMovement.GetGroundContactCount() > 0)
            {
                interactionText.enabled = true;
            }
            else
            {
                interactionText.enabled = false;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerMovement = other.GetComponent<HumanMovementAMI>();
            arcadeMachine = GetComponent<ArcadeMachineAMI>();
            
            interactionText.text = displayText;
            interactionText.enabled = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactionText.enabled = false;
            playerMovement = null;
            arcadeMachine = null;
        }
    }

    public void HideInteractionText()
    {
        interactionText.enabled = false;
    }
}