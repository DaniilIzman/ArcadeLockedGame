using UnityEngine;
using System.Collections;
public class Collectibles : MonoBehaviour
{
    [SerializeField] float FreezeTimer = 0f;
    //[SerializeField] float SpeedMultiplier = 0f;
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
    }
}