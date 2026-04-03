using UnityEngine;
public class HPCollectible : MonoBehaviour
{
    [SerializeField] int AdditionalBumps = 0;
    Scoring scoring;
    Mover mover;
    void Awake()
    {
        scoring = FindAnyObjectByType<Scoring>();
        mover = FindAnyObjectByType<Mover>();
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
    }
}