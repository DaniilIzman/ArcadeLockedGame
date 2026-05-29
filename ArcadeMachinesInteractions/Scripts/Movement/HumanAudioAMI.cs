using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class HumanAudioAMI : MonoBehaviour
{

    [Header("Footsteps")]
    [SerializeField] AudioClip[] walkStepSounds;
    [SerializeField] AudioClip[] crouchStepSounds;
    [SerializeField] float walkStepInterval   = 0.45f;
    [SerializeField] float crouchStepInterval = 0.65f;

    [Range(0f, 1f)]
    [SerializeField] float footstepVolume = 0.6f;

    [Header("Crouch")]
    [SerializeField] AudioClip crouchDownSound;
    [SerializeField] AudioClip crouchUpSound;

    [Range(0f, 1f)]
    [SerializeField] float crouchVolume = 0.8f;

    AudioSource audioSource;

    float stepTimer      = 0f;
    bool  wasMoving      = false;
    bool  wasCrouching   = false;
    int   lastStepIndex  = -1;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.playOnAwake = false;
        audioSource.loop        = false;
        audioSource.spatialBlend = 1f; // 3D sound
    }

    public void Tick(bool isMoving, bool isCrouching)
    {
        HandleCrouchSound(isCrouching);
        HandleFootsteps(isMoving, isCrouching);

        wasMoving    = isMoving;
        wasCrouching = isCrouching;
    }

    void HandleCrouchSound(bool isCrouching)
    {
        // State just changed to crouching
        if (isCrouching && !wasCrouching)
        {
            if (crouchDownSound != null)
                audioSource.PlayOneShot(crouchDownSound, crouchVolume);

            stepTimer = 0f;
        }

        if (!isCrouching && wasCrouching)
        {
            if (crouchUpSound != null)
                audioSource.PlayOneShot(crouchUpSound, crouchVolume);

            stepTimer = 0f;
        }
    }

    void HandleFootsteps(bool isMoving, bool isCrouching)
    {
        if (!isMoving)
        {
            stepTimer = 0f;
            return;
        }

        float interval;
        if (isCrouching)
            interval = crouchStepInterval;
        else
            interval = walkStepInterval;

        stepTimer += Time.deltaTime;

        if (stepTimer >= interval)
        {
            stepTimer = 0f;
            PlayFootstep(isCrouching);
        }
    }

    void PlayFootstep(bool isCrouching)
    {
        AudioClip[] clips;
        if (isCrouching)
            clips = crouchStepSounds;
        else
            clips = walkStepSounds;

        if (clips == null || clips.Length == 0)
            return;

        int index = lastStepIndex;

        if (clips.Length > 1)
        {
            while (index == lastStepIndex)
                index = Random.Range(0, clips.Length);
        }
        else
        {
            index = 0;
        }

        lastStepIndex = index;

        if (clips[index] != null)
            audioSource.PlayOneShot(clips[index], footstepVolume);
    }
}