using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
public class CollisionDetectorGG : MonoBehaviour
{
    [SerializeField] float LevelReloadDelay = 0f;
    [SerializeField] float LoadNextLevelDelay = 0f;
    [SerializeField] AudioClip VictoryAudio;
    [SerializeField] AudioClip CrashAudio;
    [SerializeField] ParticleSystem CrashParticleSystem;
    [SerializeField] ParticleSystem VictoryParticleSystem;
    AudioSource AudioSource;
    bool isControllable = true;

    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other)
    {
        if(isControllable == false)
        {
            return;
        }
        else
        {
            isControllable = true;
            switch (other.gameObject.tag)
            {
                case "Spawn":
                    Debug.Log("Spawnpoint");
                    break;
                case "Obstacle":
                    Debug.Log("Obstacle");
                    break;
                case "Finish":
                    EffectsAfterFinish();
                    break;
                default:
                    EffectsAfterCrash();
                    break;
            } 
        }

    }

    void ReloadLevelScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
    void EffectsAfterCrash()
    {
        CrashParticleSystem.Play();
        isControllable = false;
        AudioSource.Stop();
        AudioSource.PlayOneShot(CrashAudio);
        GetComponent<MovementGG>().enabled = false;
        Invoke("ReloadLevelScene", LevelReloadDelay);
    }

    void LoadNextLevelScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;
        if(nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }
        SceneManager.LoadScene(nextScene);
    }
    void EffectsAfterFinish()
    {
        VictoryParticleSystem.Play();
        isControllable = false;
        AudioSource.Stop();
        AudioSource.PlayOneShot(VictoryAudio);
        GetComponent<MovementGG>().enabled = false;
        Invoke("LoadNextLevelScene", LoadNextLevelDelay);
    }
}
