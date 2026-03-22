using UnityEngine;

public class ObjectHitAction : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Player")
        {
            GetComponent<MeshRenderer>().material.color = Color.darkOliveGreen;
            gameObject.tag = "Hit";
        }
    }
}
