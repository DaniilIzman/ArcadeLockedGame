using UnityEngine;

public class NewSectionCreator : MonoBehaviour
{
    public GameObject Tile;
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("TriggerExtender"))
        {
            Instantiate(Tile, new Vector3(7, 2, 35), Quaternion.identity);
        }
    }
    void Update()
    {
        
    }
}
