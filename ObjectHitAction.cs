using UnityEngine;

public class ObjectHitAction : MonoBehaviour
{
    [SerializeField] Color ColorHit;
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Player")
        {
            GetComponent<MeshRenderer>().material.color = ColorHit;
            gameObject.tag = "Hit";
        }
    }
}
