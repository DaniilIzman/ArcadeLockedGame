using UnityEngine;
using System.Collections;
public class SpeedModifierCollectible : MonoBehaviour
{
    [SerializeField] float SpeedMultiplier = 0f;
    [SerializeField] bool SpeedTimerActivate;
    [SerializeField] float SpeedTimer = 0f;
    Scoring scoring;
    Mover mover;
    void Awake()
    {
        scoring = FindAnyObjectByType<Scoring>();
        mover = FindAnyObjectByType<Mover>();
    }
    IEnumerator ResetSpeed(float originalSpeed, float originalRotationSpeed, float time)
    {
        yield return new WaitForSeconds(time);
        mover.moveSpeed = originalSpeed;
        mover.rotationSpeed = originalRotationSpeed;
    }
    void OnTriggerEnter(Collider other)
    {
        if(gameObject.tag == "Speed" && other.gameObject.tag == "Player")
        {
            float originalSpeed = mover.moveSpeed;
            float originalRotationSpeed = mover.rotationSpeed;
            mover.moveSpeed = mover.moveSpeed * SpeedMultiplier;
            mover.rotationSpeed = mover.rotationSpeed * SpeedMultiplier;
            if(SpeedTimerActivate == true && SpeedTimer > 0f && SpeedMultiplier > 0f)
            {
                mover.StartCoroutine(ResetSpeed(originalSpeed, originalRotationSpeed, SpeedTimer));
            }
            Destroy(gameObject);
        }
    }
}