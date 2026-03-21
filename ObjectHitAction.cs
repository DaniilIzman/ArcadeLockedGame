using UnityEngine;

public class ObjectHitAction : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        Debug.Log("Collision");
    }
}
