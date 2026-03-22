using System.Runtime.CompilerServices;
using UnityEngine;

public class Scoring : MonoBehaviour
{
    
    int bumpCounter = 0;

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag != "Hit")
        {
            bumpCounter++;
            Debug.Log("You've bumped into objects " + bumpCounter + " times");
        }

    }
}
