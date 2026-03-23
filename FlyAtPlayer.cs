using Unity.VisualScripting;
using UnityEngine;

public class FlyAtPlayer : MonoBehaviour
{
    [SerializeField] float ProjectileSpeed = 0f;
    [SerializeField] Transform player;
    Vector3 playerPosition;
    void Awake() 
    {
        gameObject.SetActive(false);
    }
    
    void Start()
    {
        playerPosition = player.transform.position;
    }

    void Update()
    {
        ShootAtPlayer();
        DestroyOnReach();
    }
    void ShootAtPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerPosition, Time.deltaTime * ProjectileSpeed);
    }
    void DestroyOnReach()
    {
        if(transform.position == playerPosition)
        {
            Destroy(gameObject);
        }
    }
}
