using UnityEngine;

public class HealthSystemGG : MonoBehaviour
{
    [SerializeField] int maxLives = 3;
    
    int currentLives;

    void Start()
    {
        currentLives = maxLives;
        Debug.Log("Lives: " + currentLives + "/" + maxLives);
    }

    public int GetCurrentLives()
    {
        return currentLives;
    }

    public int GetMaxLives()
    {
        return maxLives;
    }

    public bool TakeDamage()
    {
        currentLives--;
        Debug.Log("Lives remaining: " + currentLives + "/" + maxLives);
        return currentLives > 0;
    }

    public void AddLife()
    {
        if (currentLives < maxLives)
        {
            currentLives++;
            Debug.Log("Life restored! Lives: " + currentLives + "/" + maxLives);
        }
    }
}