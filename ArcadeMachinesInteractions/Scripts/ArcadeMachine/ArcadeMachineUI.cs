using UnityEngine;
using TMPro;

public class ArcadeMachineUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI interactionText;
    [SerializeField] string playText  = "Press E to play";
    [SerializeField] string insufficientText = "Insufficient Credits";

    ArcadeMachineAMI   arcadeMachine;
    ArcadeMachineAudio arcadeAudio;
    HumanMovementAMI   playerMovement;

    bool playerInRange    = false;
    bool lastCouldAfford  = false;
    bool entryAudioPlayed = false;

    void Start()
    {
        arcadeMachine = GetComponent<ArcadeMachineAMI>();
        arcadeAudio   = GetComponent<ArcadeMachineAudio>();

        interactionText.enabled = false;
    }

    void Update()
    {
        if (!playerInRange || playerMovement == null) return;

        bool canAfford = CanAfford();

        RefreshText(canAfford);

        if (canAfford != lastCouldAfford)
        {
            if (canAfford && !entryAudioPlayed)
            {
                if (arcadeAudio != null)
                    arcadeAudio.PlayTextAppearSound();

                entryAudioPlayed = true;
            }
            else if (!canAfford)
            {
                if (arcadeAudio != null)
                    arcadeAudio.PlayInsufficientCreditsSound();
            }

            arcadeMachine.SetInteractable(canAfford);
            lastCouldAfford = canAfford;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        playerMovement   = other.GetComponent<HumanMovementAMI>();
        playerInRange    = true;
        entryAudioPlayed = false;
        lastCouldAfford  = !CanAfford();

        interactionText.enabled = true;

        if (CanAfford())
        {
            if (arcadeAudio != null)
                arcadeAudio.PlayTextAppearSound();

            entryAudioPlayed = true;
        }
        else
        {
            if (arcadeAudio != null)
                arcadeAudio.PlayInsufficientCreditsSound();
        }

        arcadeMachine.SetInteractable(CanAfford());
        lastCouldAfford = CanAfford();
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        playerMovement   = null;
        playerInRange    = false;
        entryAudioPlayed = false;

        interactionText.enabled = false;
        arcadeMachine.SetInteractable(true);
    }

    void RefreshText(bool canAfford)
    {
        if (canAfford)
        {
            interactionText.text  = playText + " (Cost: " + arcadeMachine.GetGameCost() + ")";
            interactionText.color = Color.white;
        }
        else
        {
            interactionText.text  = insufficientText + " (Need: " + arcadeMachine.GetGameCost() + ")";
            interactionText.color = Color.red;
        }
    }

    bool CanAfford()
    {
        if (PlayerCreditsAMI.instance == null) return false;
        return PlayerCreditsAMI.instance.GetCredits() >= arcadeMachine.GetGameCost();
    }

    public void HideInteractionText()
    {
        enabled = false;
        interactionText.enabled = false;
    }
}