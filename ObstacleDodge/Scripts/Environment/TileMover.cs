using UnityEngine;

public class TileMover : MonoBehaviour
{

    void Update()
    {
        transform.position += new Vector3(0, 0, -2) * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("TriggerDestroyer"))
        {
            Destroy(gameObject);
        }
    }

}