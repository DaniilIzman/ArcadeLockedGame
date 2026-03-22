using UnityEngine;

public class DropperObstacle : MonoBehaviour
{
    [SerializeField] float TimeBeforeFall = 3f;
    void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }

    void Update()
    {
        if(Time.time > TimeBeforeFall)
        {
            Debug.Log("Lookout!");
        }
    }

}
