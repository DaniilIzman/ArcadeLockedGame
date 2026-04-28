using Unity.VisualScripting;
using UnityEngine;

public class Spin : MonoBehaviour
{
    [SerializeField] float RotateX = 0f;
    [SerializeField] float RotateY = 0f;
    [SerializeField] float RotateZ = 0f;
    
    void Update()
    {
        transform.Rotate(RotateX, RotateY, RotateZ);
    }
}
