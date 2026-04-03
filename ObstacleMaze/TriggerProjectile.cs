using System.Collections.Generic;
using UnityEngine;

public class TriggerProjectile : MonoBehaviour
{
    [SerializeField] List<GameObject> GameObjectList;

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            for(int i = 0; i < GameObjectList.Count; i++)
            {
                GameObjectList[i].SetActive(true);
            }
            Destroy(gameObject);
        }
    }
}
