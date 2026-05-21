using UnityEngine;

public class EnemyCF : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] int hitPoints = 1;
    [SerializeField] int scoreValue = 10;

    [Header("VFX")]
    [SerializeField] GameObject destroyVFX;

    Scoreboard scoreboard;

    void Start()
    {
        scoreboard = FindFirstObjectByType<Scoreboard>();
    }

    void OnParticleCollision(GameObject other)
    {
        TakeDamage();
    }

    void TakeDamage()
    {
        hitPoints--;

        if (hitPoints <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        scoreboard.modifyScore(scoreValue);

        Instantiate(destroyVFX, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}