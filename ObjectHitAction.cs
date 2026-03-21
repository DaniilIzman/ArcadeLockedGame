using UnityEngine;

public class ObjectHitAction : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        GetComponent<MeshRenderer>().material.color = Color.darkOliveGreen;
        Debug.Log("Collision");
    }
}
