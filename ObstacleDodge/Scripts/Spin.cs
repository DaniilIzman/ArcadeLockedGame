using Unity.VisualScripting;
using UnityEngine;

public class Spin : MonoBehaviour
{
    [SerializeField] float RotateX = 0f;
    [SerializeField] float RotateY = 0f;
    [SerializeField] float RotateZ = 0f;
    ObjectHitAction HitAction;
    void Awake()
    {
        HitAction = GetComponent<ObjectHitAction>();
    }
    void Update()
    {
        transform.Rotate(RotateX, RotateY, RotateZ);
    }
}
