using UnityEngine;

public class ArcadeMachineAudio : MonoBehaviour
{
    [Header("Audio Clips")]
    [SerializeField] AudioClip approachSound;
    [SerializeField] AudioClip startSound;
    [SerializeField] AudioClip errorSound;

    [Header("Audio Settings")]
    [SerializeField] float soundVolume = 0.7f;
    [SerializeField] int audioPriority = 128;

    AudioSource audioSource;
    bool hasPlayedApproachSound = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.volume = soundVolume;
        audioSource.priority = audioPriority;
        audioSource.spatialBlend = 0.5f;

        Debug.Log("ArcadeMachineAudio initialized");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!hasPlayedApproachSound)
            {
                PlayApproachSound();
                hasPlayedApproachSound = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hasPlayedApproachSound = false;
        }
    }

    public void PlayStartSound()
    {
        if (startSound != null)
        {
            audioSource.PlayOneShot(startSound, soundVolume);
            Debug.Log("Start sound played");
        }
        else
        {
            Debug.LogWarning("Start sound not assigned!");
        }
    }

    public void PlayErrorSound()
    {
        if (errorSound != null)
        {
            audioSource.PlayOneShot(errorSound, soundVolume);
            Debug.Log("Error sound played");
        }
        else
        {
            Debug.LogWarning("Error sound not assigned!");
        }
    }

    void PlayApproachSound()
    {
        if (approachSound != null)
        {
            audioSource.PlayOneShot(approachSound, soundVolume);
            Debug.Log("Approach sound played");
        }
    }
}