using UnityEngine;

public class EnemyCF : MonoBehaviour
{
    void OnParticleCollision(GameObject other)
    {
        Destroy(gameObject);
    }
}
