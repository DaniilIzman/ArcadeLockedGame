using UnityEngine;

public class Collectables : MonoBehaviour
{
    [SerializeField] float FreezeTimer = 0f;
    [SerializeField] float SpeedMultiplier = 0f;
    [SerializeField] int AdditionalBumps = 0;
    [SerializeField] float InvulnerabilityTimer = 0f;
    void OnTriggerEnter(Collider other)
    {
        if(gameObject.tag == "HP" && other.gameObject.tag == "Player")
        {
            Scoring scoring = FindAnyObjectByType<Scoring>();
            scoring.bumpCounter -= AdditionalBumps;
            scoring.bumpCounter = Mathf.Max(scoring.bumpCounter, 0);
            Destroy(gameObject);
        }
    }
}
