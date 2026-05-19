using UnityEngine;

public class HumanCameraFollow : MonoBehaviour
{
    public Transform cameraFollowTarget;
    void Update()
    {
        transform.position = cameraFollowTarget.position;
    }
}
