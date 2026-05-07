using UnityEngine;

public class CollisionDetectorCF : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit object: " + other.name);
    }
}
