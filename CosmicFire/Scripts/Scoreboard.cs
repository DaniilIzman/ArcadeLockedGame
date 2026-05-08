using UnityEngine;
using TMPro;

public class Scoreboard : MonoBehaviour
{
    int score = 0;
    [SerializeField] TMP_Text scoreDisplay;
    public void modifyScore(int amount)
    {
        score += amount;
        scoreDisplay.text = score.ToString();
    }
}
