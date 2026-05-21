using UnityEngine;

public class CollisionDetectorCF : MonoBehaviour
{
    [Header("Effects")]
    [SerializeField] GameObject destroyVFX;

    GameSceneManagerCF gameSceneManager;

    void Start()
    {
        gameSceneManager = FindFirstObjectByType<GameSceneManagerCF>();
    }

    void OnTriggerEnter(Collider other)
    {
        HandleHit();
    }

    void HandleHit()
    {
        if (gameSceneManager != null)
        {
            gameSceneManager.ReloadLevel();
        }

        if (destroyVFX != null)
        {
            Instantiate(destroyVFX, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}