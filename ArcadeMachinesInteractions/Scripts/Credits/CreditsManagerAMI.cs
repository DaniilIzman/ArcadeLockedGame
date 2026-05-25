using UnityEngine;
using TMPro;

public class CreditsManagerAMI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI creditsText;

    void Start()
    {
        if (creditsText == null)
        {
            Debug.LogError("CreditsText not assigned!");
            return;
        }
    }

    void Update()
    {
        if (PlayerCreditsAMI.instance != null)
        {
            creditsText.text = "Credits: " + PlayerCreditsAMI.instance.GetCredits();
        }
    }
}