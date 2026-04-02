using UnityEngine;
using System.Collections;
public class Collectibles : MonoBehaviour
{
    [SerializeField] float FreezeTimer = 0f;
    [SerializeField] float SpeedMultiplier = 0f;
    [SerializeField] bool SpeedTimerActivate;
    [SerializeField] float SpeedTimer = 0f;
    [SerializeField] int AdditionalBumps = 0;
    //[SerializeField] float InvulnerabilityTimer = 0f;
    Scoring scoring;
    Mover mover;
    void Awake()
    {
        scoring = FindAnyObjectByType<Scoring>();
        mover = FindAnyObjectByType<Mover>();
    }
    IEnumerator UnfreezeAfter(float time)
    {
        yield return new WaitForSeconds(time);
        mover.canMove = true;
    }
    IEnumerator ResetSpeed(float originalSpeed, float originalRotationSpeed, float time)
    {
        yield return new WaitForSeconds(time);
        mover.moveSpeed = originalSpeed;
        mover.rotationSpeed = originalRotationSpeed;
    }
    void OnTriggerEnter(Collider other)
    {
        if(gameObject.tag == "HP" && other.gameObject.tag == "Player" && scoring.BumpMaxActivate == true)
        {
            if(scoring.bumpCounter > 0)
            {
                scoring.bumpCounter -= AdditionalBumps;
                scoring.bumpCounter = Mathf.Max(scoring.bumpCounter, 0);
                Destroy(gameObject);
            }
        }
        else if(gameObject.tag == "Freezer" && other.gameObject.tag == "Player")
        {
            mover.canMove = false;
            mover.StartCoroutine(UnfreezeAfter(FreezeTimer));
            Destroy(gameObject);
        }
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