using UnityEngine;

public class CreditCalculatorGG : MonoBehaviour
{
    [SerializeField] float scoreToCredit = 0.2f;

    public int CalculateCredits(int score)
    {
        int creditsEarned = Mathf.FloorToInt(score * scoreToCredit);
        
        Debug.Log("Credits earned: " + creditsEarned + " (score: " + score + ")");
        
        return creditsEarned;
    }
}