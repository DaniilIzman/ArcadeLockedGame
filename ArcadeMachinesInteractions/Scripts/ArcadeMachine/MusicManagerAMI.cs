using UnityEngine;

public class MusicManagerAMI : MonoBehaviour
{
    [Header("Playlist")]
    [SerializeField] private AudioClip[] clips;

    [Range(0f, 1f)]
    [SerializeField] private float volume = 0.5f;

    [Header("Settings")]
    [SerializeField] private bool shuffleClips = true;

    private AudioSource audioSource;
    private int currentClipIndex = -1;

    void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = false;
        audioSource.playOnAwake = false;
        audioSource.volume = volume;
    }

    void Start()
    {
        if (clips != null && clips.Length > 0)
        {
            if (shuffleClips)
            {
                ShuffleClips(clips);
            }
            PlayNextClip();
        }
    }

    void Update()
    {
        // Automatically play the next clip when the current one finishes
        if (clips != null && clips.Length > 0 && !audioSource.isPlaying)
        {
            PlayNextClip();
        }
    }

    private void PlayNextClip()
    {
        if (clips == null || clips.Length == 0) return;

        currentClipIndex = (currentClipIndex + 1) % clips.Length;

        AudioClip clip = clips[currentClipIndex];
        if (clip == null) return;

        audioSource.clip = clip;
        audioSource.Play();
    }

    private void ShuffleClips(AudioClip[] clipsToShuffle)
    {
        for (int i = clipsToShuffle.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            AudioClip temp = clipsToShuffle[i];
            clipsToShuffle[i] = clipsToShuffle[j];
            clipsToShuffle[j] = temp;
        }
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }

    public void SetVolume(float newVolume)
    {
        volume = Mathf.Clamp01(newVolume);
        audioSource.volume = volume;
    }
}