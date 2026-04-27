using UnityEngine;

public class NewTileCreator : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        TriggerExtender extender = other.GetComponent<TriggerExtender>();
        Vector3 spawnPosition = extender.transform.position + extender.SpawnOffset;
        if(other.gameObject.CompareTag("TriggerExtender"))
        {
            Instantiate(extender.TilePrefab, spawnPosition, Quaternion.identity);      
        }
    }
}