using UnityEngine;

public class ArcadeMachineAudio : MonoBehaviour
{
    [Header("Audio Clips")]
    [SerializeField] AudioClip textAppearSound;
    [SerializeField] AudioClip insufficientCreditsSound;
    [SerializeField] AudioClip confirmSound;

    [Header("Audio Settings")]
    [SerializeField] float soundVolume = 0.7f;
    [SerializeField] int audioPriority = 128;

    AudioSource audioSource;

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

    public void PlayTextAppearSound()
    {
        if (textAppearSound != null)
        {
            audioSource.PlayOneShot(textAppearSound, soundVolume);
            Debug.Log("Text appear sound played");
        }
    }

    public void PlayInsufficientCreditsSound()
    {
        if (insufficientCreditsSound != null)
        {
            audioSource.PlayOneShot(insufficientCreditsSound, soundVolume);
            Debug.Log("Insufficient credits sound played");
        }
    }

    public void PlayConfirmSound()
    {
        if (confirmSound != null)
        {
            audioSource.PlayOneShot(confirmSound, soundVolume);
            Debug.Log("Confirm sound played");
        }
    }
}