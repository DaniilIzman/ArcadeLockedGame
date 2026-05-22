using UnityEngine;
using UnityEngine.InputSystem;

public class CollisionDetectorGG : MonoBehaviour
{
    [Header("Scene Delays")]
    [SerializeField] float levelReloadDelay = 0f;
    [SerializeField] float loadNextLevelDelay = 0f;

    [Header("Audio")]
    [SerializeField] AudioClip victoryAudio;
    [SerializeField] AudioClip crashAudio;

    [Header("Particles")]
    [SerializeField] ParticleSystem crashParticleSystem;
    [SerializeField] ParticleSystem victoryParticleSystem;

    [Header("Debug")]
    [SerializeField] bool isCollidable = true;

    AudioSource audioSource;
    MovementGG movement;
    bool isControllable = true;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        movement = GetComponent<MovementGG>();
    }

    void Update()
    {
        Debugging();
    }

    void OnCollisionEnter(Collision other)
    {
        if (!isControllable || !isCollidable)
            return;

        switch (other.gameObject.tag)
        {
            case "Spawn":
                Debug.Log("Spawnpoint");
                break;

            case "Finish":
                EffectsAfterFinish();
                break;

            default:
                EffectsAfterCrash();
                break;
        }
    }

    void EffectsAfterCrash()
    {
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(crashAudio);
        crashParticleSystem.Play();
        movement.enabled = false;
        SceneTransitionManagerAMI.instance.ReloadCurrentScene(levelReloadDelay);
    }

    void EffectsAfterFinish()
    {
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(victoryAudio);
        victoryParticleSystem.Play();
        movement.enabled = false;
        SceneTransitionManagerAMI.instance.LoadNextScene(loadNextLevelDelay);
    }

    void Debugging()
    {
        if (Keyboard.current.lKey.wasPressedThisFrame)
            SceneTransitionManagerAMI.instance.LoadNextScene();

        else if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            isCollidable = !isCollidable;
            Debug.Log("Collisions: " + isCollidable);
        }
    }
}