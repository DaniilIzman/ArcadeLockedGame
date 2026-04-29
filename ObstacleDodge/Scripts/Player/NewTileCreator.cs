using UnityEngine;

public class NewTileCreator : MonoBehaviour
{
    public float SpawnRangeX = 3f;
    public float SpawnRangeZ = 3f;

    void OnTriggerEnter(Collider other)
    {
        TriggerExtender extender = other.GetComponent<TriggerExtender>();
        Vector3 spawnPosition = extender.transform.position + extender.SpawnOffset;

        if (other.gameObject.CompareTag("TriggerExtender"))
        {
            Instantiate(extender.TilePrefab, spawnPosition, Quaternion.identity);

            if (extender.Obstacles != null && extender.Obstacles.Length > 0)
            {
                foreach (GameObject obstacle in extender.Obstacles)
                {
                    Vector3 obstacleSpawnPos = new Vector3(
                    spawnPosition.x + Random.Range(-SpawnRangeX, SpawnRangeX),spawnPosition.y, spawnPosition.z + Random.Range(-SpawnRangeZ, SpawnRangeZ));
                    Instantiate(obstacle, obstacleSpawnPos, Quaternion.identity);
                }
            }

            if (extender.Collectibles != null && extender.Collectibles.Length > 0)
            {
                foreach (GameObject collectible in extender.Collectibles)
                {
                    Vector3 collectibleSpawnPos = new Vector3(
                        spawnPosition.x + Random.Range(-SpawnRangeX, SpawnRangeX),spawnPosition.y,spawnPosition.z + Random.Range(-SpawnRangeZ, SpawnRangeZ));
                    Instantiate(collectible, collectibleSpawnPos, Quaternion.identity);
                }
            }
        }
    }
}