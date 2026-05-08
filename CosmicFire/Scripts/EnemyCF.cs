using UnityEngine;

public class EnemyCF : MonoBehaviour
{
    [SerializeField] GameObject destroyVFX;
    void OnParticleCollision(GameObject other)
    {
        Instantiate(destroyVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
