using UnityEngine;

public class DropperObstacle : MonoBehaviour
{
    [SerializeField] float TimeBeforeFall = 3f;
    void Start()
    {
        
    }

    void Update()
    {
        if(Time.time > TimeBeforeFall)
        {
            Debug.Log("Lookout!");
        }
    }

}
