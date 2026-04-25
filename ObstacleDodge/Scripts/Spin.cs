using Unity.VisualScripting;
using UnityEngine;

public class Spin : MonoBehaviour
{
    [SerializeField] float RotateX = 0f;
    [SerializeField] float RotateY = 0f;
    [SerializeField] float RotateZ = 0f;
    [SerializeField] bool SlowAfterHit;
    [SerializeField] float SpeedMultiplier = 0f;
    float CurrentMultiplier = 1f;
    ObjectHitAction HitAction;
    void Awake()
    {
        HitAction = GetComponent<ObjectHitAction>();
    }
    void Update()
    {
        if(SlowAfterHit)
        {
            if(HitAction.ObjectTouched)
            {
                CurrentMultiplier = SpeedMultiplier;
            }
            else
            {
                CurrentMultiplier = 1f;   
            }

        }
        transform.Rotate(RotateX * CurrentMultiplier, RotateY * CurrentMultiplier, RotateZ * CurrentMultiplier);
    }
}
