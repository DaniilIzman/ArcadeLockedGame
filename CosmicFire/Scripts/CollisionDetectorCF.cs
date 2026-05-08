using UnityEngine;

public class CollisionDetectorCF : MonoBehaviour
{
    [SerializeField] GameObject destroyVFX;
    
    void OnTriggerEnter(Collider other)
    {
        Instantiate(destroyVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
