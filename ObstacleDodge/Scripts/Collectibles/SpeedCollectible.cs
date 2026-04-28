using UnityEngine;
using System.Collections;
public class SpeedCollectible : MonoBehaviour
{
    [SerializeField] float SpeedMultiplier = 0f;
    [SerializeField] bool SpeedTimerActivate;
    [SerializeField] float SpeedTimer = 0f;
    MovementOD PlayerMovement;
    void Awake()
    {
        PlayerMovement = FindAnyObjectByType<MovementOD>();
    }
    IEnumerator ResetSpeed(float originalSpeed, float originalRotationSpeed, float time)
    {
        yield return new WaitForSeconds(time);
        PlayerMovement.strafeSpeed = originalSpeed;
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            float originalSpeed = PlayerMovement.strafeSpeed;
            PlayerMovement.strafeSpeed = PlayerMovement.strafeSpeed * SpeedMultiplier;
            if(SpeedTimerActivate == true && SpeedTimer > 0f && SpeedMultiplier > 0f)
            {
                PlayerMovement.StartCoroutine(ResetSpeed(originalSpeed, 0f, SpeedTimer));
            }
            Destroy(gameObject);
        }
    }
}