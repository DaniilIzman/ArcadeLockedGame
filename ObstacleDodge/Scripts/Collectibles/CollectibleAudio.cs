using UnityEngine;

public class CollectibleAudio : MonoBehaviour
{
    [SerializeField] AudioClip collectSound;
    [SerializeField] float volume = 0.7f;

    public void PlayCollectSound()
    {
        if (collectSound == null)
        {
            Debug.LogError("AudioClip missing!");
            return;
        }

        GameObject tempAudioObject = new GameObject("CollectibleSound");
        tempAudioObject.transform.position = transform.position;

        AudioSource tempAudioSource = tempAudioObject.AddComponent<AudioSource>();
        tempAudioSource.clip = collectSound;
        tempAudioSource.volume = volume;
        tempAudioSource.spatialBlend = 0f;
        tempAudioSource.Play();

        Destroy(tempAudioObject, collectSound.length);

        Debug.Log("Playing sound: " + collectSound.name);
    }
}