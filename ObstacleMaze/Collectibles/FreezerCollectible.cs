using UnityEngine;
using System.Collections;
public class FreezerCollectible : MonoBehaviour
{
    [SerializeField] float FreezeTimer = 0f;
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
        if(other.CompareTag("Player"))
        {
            mover.canMove = false;
            mover.StartCoroutine(UnfreezeAfter(FreezeTimer));
            Destroy(gameObject);
        }
    }
}