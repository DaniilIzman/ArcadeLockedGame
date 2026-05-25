using UnityEngine;

public class PlayerCreditsAMI : MonoBehaviour
{
    public static PlayerCreditsAMI instance;
    
    int credits = 100;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("PlayerCredits initialized with " + credits + " credits");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int GetCredits()
    {
        return credits;
    }

    public void AddCredits(int amount)
    {
        credits += amount;
        Debug.Log("Total Credits: " + credits);
    }

    public bool SpendCredits(int amount)
    {
        if (credits >= amount)
        {
            credits -= amount;
            return true;
        }
        return false;
    }
}