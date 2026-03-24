using Unity.VisualScripting;
using UnityEngine;

public class FlyAtPlayer : MonoBehaviour
{
    [SerializeField] float ProjectileSpeed = 0f;
    [SerializeField] Transform player;
    [SerializeField] bool FollowPlayer;
    Vector3 playerPosition;
    Vector3 constantPlayerPosition;
    
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
        constantPlayerPosition = player.transform.position;
        if(FollowPlayer == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, constantPlayerPosition, Time.deltaTime * ProjectileSpeed);  
        }
        else
        {
            ShootAtPlayer();
        }
        DestroyOnReach();
    }
    void ShootAtPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerPosition, Time.deltaTime * ProjectileSpeed);
    }
    void DestroyOnReach()
    {
        if(transform.position == playerPosition || Vector3.Distance(transform.position,constantPlayerPosition)  < 0.35f)
        {
            Destroy(gameObject);
        }
    }
}
