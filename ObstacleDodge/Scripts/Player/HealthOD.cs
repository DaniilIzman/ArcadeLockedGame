using UnityEngine;

public class HealthOD : MonoBehaviour
{
    [SerializeField] private int BumpMax = 3;
    public bool BumpMaxActivate = true;
    public int bumpCounter = 0;
    
    public bool isInvulnerable = false;

    private void OnCollisionEnter(Collision other)
    {
        if (!BumpMaxActivate || isInvulnerable) return;

        if (!other.gameObject.CompareTag("Hit") && !other.gameObject.CompareTag("Ground"))
        {
            bumpCounter++;
            Debug.Log("You've bumped into objects " + bumpCounter + " times"); 

            if (bumpCounter >= BumpMax)
            {
                Debug.Log("Game Over!");
                Time.timeScale = 0f;
            }
        }
    }
}