using UnityEngine;

public class Collectibles : MonoBehaviour
{
    [SerializeField] float FreezeTimer = 0f;
    //[SerializeField] float SpeedMultiplier = 0f;
    [SerializeField] int AdditionalBumps = 0;
    //[SerializeField] float InvulnerabilityTimer = 0f;
    bool frozen = false;
    Scoring scoring;
    Mover mover;
    void Awake()
    {
        scoring = FindAnyObjectByType<Scoring>();
        mover = FindAnyObjectByType<Mover>();
    }
    void Update()
    {
        if(frozen == true && FreezeTimer > 0f)
        {
            FreezeTimer -= Time.deltaTime;
            if(FreezeTimer <= 0f)
            {
                frozen = false;
                mover.canMove = true;
                Destroy(gameObject);
            }
        }
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
            frozen = true;
        }
    }
}