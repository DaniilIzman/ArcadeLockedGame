using UnityEngine;

public class CollectibleAudio : MonoBehaviour
{
    [SerializeField] AudioClip collectSound;
    [SerializeField] float volume = 0.7f;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void PlayCollectSound()
    {
        if (collectSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(collectSound, volume);
        }
    }
}