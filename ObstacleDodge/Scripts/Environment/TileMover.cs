using UnityEngine;

public class TileDestroyer : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        transform.position += new Vector3(0, 0, -2) * Time.deltaTime;

    }
}
