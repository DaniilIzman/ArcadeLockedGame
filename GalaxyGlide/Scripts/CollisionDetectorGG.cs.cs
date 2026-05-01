using UnityEngine;

public class CollisionDetectorGG : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Spawn":
                Debug.Log("Spawnpoint");
                break;
            case "Obstacle":
                Debug.Log("Obstacle");
                break;
            case "Finish":
                Debug.Log("Finish");
                break;
            default:
                Debug.Log("Bumped into unidentified object!");
                break;
        }
    }
}
