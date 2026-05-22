using UnityEngine;
using TMPro;

public class ArcadeMachineUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI interactionText;
    [SerializeField] string displayText = "Press E to play";

    void Start()
    {
        interactionText.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactionText.text = displayText;
            interactionText.enabled = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactionText.enabled = false;
        }
    }
}