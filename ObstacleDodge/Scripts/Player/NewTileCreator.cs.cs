using UnityEngine;

public class NewSectionCreator : MonoBehaviour
{
    public GameObject Tile;
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("TriggerExtender"))
        {
            Instantiate(Tile, new Vector3(7, 2, 42), Quaternion.identity);
        }
    }
    void Update()
    {
        
    }
}