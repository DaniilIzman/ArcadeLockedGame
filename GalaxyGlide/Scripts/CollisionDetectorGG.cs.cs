using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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
        {
            return;
        }

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

        Invoke(nameof(ReloadLevelScene), levelReloadDelay);
    }

    void EffectsAfterFinish()
    {
        isControllable = false;

        audioSource.Stop();
        audioSource.PlayOneShot(victoryAudio);

        victoryParticleSystem.Play();

        movement.enabled = false;

        Invoke(nameof(LoadNextLevelScene), loadNextLevelDelay);
    }

    void ReloadLevelScene()
    {
        int currentScene =
            SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(currentScene);
    }

    void LoadNextLevelScene()
    {
        int currentScene =
            SceneManager.GetActiveScene().buildIndex;

        int nextScene = currentScene + 1;

        if (nextScene >= SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }

        SceneManager.LoadScene(nextScene);
    }

    void Debugging()
    {
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            LoadNextLevelScene();
        }

        else if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            isCollidable = !isCollidable;

            Debug.Log("Collisions: " + isCollidable);
        }
    }
}