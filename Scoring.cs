using UnityEngine;

public class ObjectHitAction : MonoBehaviour
{
    int bumpCounter = 0;

    void OnCollisionEnter(Collision collision)
    {
        bumpCounter++;
        Debug.Log("You've bumped into objects " + bumpCounter + " times");
    }
}
