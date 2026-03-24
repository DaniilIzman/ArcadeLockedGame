using System.Runtime.CompilerServices;
using UnityEngine;

public class Scoring : MonoBehaviour
{
    
    [SerializeField] int BumpMax;
    int bumpCounter = 0;

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag != "Hit")
        {
            bumpCounter++;
            Debug.Log("You've bumped into objects " + bumpCounter + " times");
            if(bumpCounter == BumpMax)
            {
                Debug.Log("Game Over!");
                Time.timeScale = 0f;
            }
        }

    }
}
