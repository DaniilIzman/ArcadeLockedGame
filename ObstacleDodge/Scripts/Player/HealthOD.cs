using System.Runtime.CompilerServices;
using UnityEngine;

public class HealthOD : MonoBehaviour
{
    [SerializeField] int BumpMax;
    public bool BumpMaxActivate;
    public int bumpCounter = 0;
    void OnCollisionEnter(Collision other)
    {
        if(!other.gameObject.CompareTag("Hit") && !other.gameObject.CompareTag("Ground") && BumpMaxActivate)
        {
            bumpCounter++;
            Debug.Log("You've bumped into objects " + bumpCounter + " times"); 
            if(bumpCounter >= BumpMax)
            {
                Debug.Log("Game Over!");
                Time.timeScale = 0f;
            }
        }
    }
}
