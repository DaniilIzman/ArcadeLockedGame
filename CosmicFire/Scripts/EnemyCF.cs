using UnityEngine;

public class EnemyCF : MonoBehaviour
{
    [SerializeField] GameObject destroyVFX;
    [SerializeField] int hitPoints = 1;
    void OnParticleCollision(GameObject other)
    {
        EnemyHP();
    }

    private void EnemyHP()
    {
        hitPoints = hitPoints - 1;
        if (hitPoints <= 0)
        {
            Instantiate(destroyVFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
