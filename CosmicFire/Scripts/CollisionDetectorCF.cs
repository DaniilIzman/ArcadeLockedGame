using UnityEngine;

public class CollisionDetectorCF : MonoBehaviour
{
    [SerializeField] GameObject destroyVFX;
    GameSceneManagerCF gameSceneManagerCF;

    void Start()
    {
        gameSceneManagerCF = FindFirstObjectByType<GameSceneManagerCF>();
    }

    void OnTriggerEnter(Collider other)
    {
        gameSceneManagerCF.ReloadLevel();
        Instantiate(destroyVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
