using UnityEngine;

public class EnemyCF : MonoBehaviour
{
    [SerializeField] GameObject destroyVFX;
    [SerializeField] int hitPoints = 1;
    [SerializeField] int scoreValue = 10;
    Scoreboard scoreboard;
    void Start()
    {
        scoreboard = FindFirstObjectByType<Scoreboard>();
    }

    void OnParticleCollision(GameObject other)
    {
        EnemyHP();
    }

    private void EnemyHP()
    {
        hitPoints = hitPoints - 1;
        if (hitPoints <= 0)
        {
            scoreboard.modifyScore(scoreValue);
            Instantiate(destroyVFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
