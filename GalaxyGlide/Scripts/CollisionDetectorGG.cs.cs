using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Collections;

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

    AudioSource audioSource;
    MovementGG movement;
    bool isControllable = true;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        movement = GetComponent<MovementGG>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (!isControllable)
            return;

        switch (other.gameObject.tag)
        {
            case "Spawn":
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

        StartCoroutine(ReloadLevelCoroutine());
    }

    void EffectsAfterFinish()
    {
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(victoryAudio);
        victoryParticleSystem.Play();
        movement.enabled = false;

        StartCoroutine(LoadNextLevelCoroutine());
    }

    IEnumerator ReloadLevelCoroutine()
    {
        yield return new WaitForSeconds(levelReloadDelay);
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    IEnumerator LoadNextLevelCoroutine()
    {
        yield return new WaitForSeconds(loadNextLevelDelay);
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;

        if (nextScene >= SceneManager.sceneCountInBuildSettings)
            nextScene = 0;

        SceneManager.LoadScene(nextScene);
    }
}