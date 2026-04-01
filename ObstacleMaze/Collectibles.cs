using UnityEngine;

public class Collectibles : MonoBehaviour
{
    //[SerializeField] float FreezeTimer = 0f;
    //[SerializeField] float SpeedMultiplier = 0f;
    [SerializeField] int AdditionalBumps = 0;
    //[SerializeField] float InvulnerabilityTimer = 0f;
    void OnTriggerEnter(Collider other)
    {
        Scoring scoring = FindAnyObjectByType<Scoring>();
        if(gameObject.tag == "HP" && other.gameObject.tag == "Player" && scoring.BumpMaxActivate == true)
        {
            scoring.bumpCounter -= AdditionalBumps;
            scoring.bumpCounter = Mathf.Max(scoring.bumpCounter, 0);
            Destroy(gameObject);
        }
        else if(other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
