using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class MovementGG : MonoBehaviour
{

    [Header("Input")]
    [SerializeField] private InputAction up;
    [SerializeField] private InputAction rotation;

    [Header("Movement")]
    [SerializeField] private float thrustForce = 500f;
    [SerializeField] private float rotationForce = 100f;

    [Tooltip("Maximum Y position the player can reach.")]
    [SerializeField] private float maxHeight = 100f;

    [Header("Space Feel")]
    [SerializeField, Range(0f, 1f)] private float linearDamping = 0.02f;
    [SerializeField, Range(0f, 1f)] private float angularDamping = 0.08f;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip engineLoopAudio;
    [SerializeField] private AudioClip thrustAudio;
    [SerializeField] private AudioClip rotateLeftAudio;
    [SerializeField] private AudioClip rotateRightAudio;

    [Header("Audio Volume")]
    [SerializeField, Range(0f, 1f)] private float engineVolume = 0.4f;
    [SerializeField, Range(0f, 1f)] private float thrustVolume = 0.8f;
    [SerializeField, Range(0f, 1f)] private float rotationVolume = 0.6f;

    [Header("Particles")]
    [SerializeField] private ParticleSystem mainThrustParticleSystem;
    [SerializeField] private ParticleSystem leftThrustParticleSystem;
    [SerializeField] private ParticleSystem rightThrustParticleSystem;
    private Rigidbody rb;

    private AudioSource engineSource;
    private AudioSource thrustSource;
    private AudioSource rotationSource;

    private bool isThrusting;
    private bool canThrust;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        rb.freezeRotation = true;

        engineSource = AddLoopingAudioSource(engineVolume);
        thrustSource = AddLoopingAudioSource(thrustVolume);
        rotationSource = AddLoopingAudioSource(rotationVolume);
    }

    private void Start()
    {
        PlayAudioSource(engineSource, engineLoopAudio);
    }

    private void OnEnable()
    {
        up.Enable();
        rotation.Enable();
    }

    private void OnDisable()
    {
        up.Disable();
        rotation.Disable();

        StopAllAudio();
        StopAllParticles();
    }

    private void FixedUpdate()
    {
        canThrust = transform.position.y < maxHeight;

        HandleThrust();
        HandleRotation();
        ApplyDamping();
        ClampHeight();
    }

    private void HandleThrust()
    {
        bool thrustHeld = up.IsPressed();

        if (thrustHeld)
        {
            PlayAudioSource(thrustSource, thrustAudio);
        }
        else if (thrustSource.isPlaying)
        {
            thrustSource.Stop();
        }

        if (thrustHeld && canThrust)
        {
            rb.AddRelativeForce(Vector3.up * thrustForce * Time.fixedDeltaTime);

            PlayParticle(mainThrustParticleSystem);

            isThrusting = true;
        }
        else
        {
            StopParticle(mainThrustParticleSystem);

            isThrusting = false;
        }
    }

    private void HandleRotation()
    {
        float input = rotation.ReadValue<float>();

        if (input < 0f)
        {
            ApplyRotation(rotationForce);
            PlayRotationSound(rotateRightAudio);

            if (isThrusting)
            {
                StopParticle(leftThrustParticleSystem);
                PlayParticle(rightThrustParticleSystem);
            }
            else
            {
                StopParticle(leftThrustParticleSystem);
                StopParticle(rightThrustParticleSystem);
            }
        }
        else if (input > 0f)
        {
            ApplyRotation(-rotationForce);
            PlayRotationSound(rotateLeftAudio);

            if (isThrusting)
            {
                StopParticle(rightThrustParticleSystem);
                PlayParticle(leftThrustParticleSystem);
            }
            else
            {
                StopParticle(leftThrustParticleSystem);
                StopParticle(rightThrustParticleSystem);
            }
        }
        else
        {
            rotationSource.Stop();

            if (isThrusting)
            {
                PlayParticle(leftThrustParticleSystem);
                PlayParticle(rightThrustParticleSystem);
            }
            else
            {
                StopParticle(leftThrustParticleSystem);
                StopParticle(rightThrustParticleSystem);
            }
        }
    }

    private void ApplyRotation(float amount)
    {
        transform.Rotate(Vector3.right * amount * Time.fixedDeltaTime);
    }

    private void PlayRotationSound(AudioClip clip)
    {
        if (clip == null) return;

        if (rotationSource.clip != clip || !rotationSource.isPlaying)
        {
            rotationSource.clip = clip;
            rotationSource.volume = rotationVolume;
            rotationSource.Play();
        }
    }

    private void ClampHeight()
    {
        if (transform.position.y <= maxHeight)
            return;

        Vector3 pos = transform.position;
        pos.y = maxHeight;
        transform.position = pos;

        Vector3 velocity = rb.linearVelocity;

        if (velocity.y > 0f)
        {
            velocity.y = 0f;
            rb.linearVelocity = velocity;
        }
    }

    private void ApplyDamping()
    {
        rb.linearVelocity *= (1f - linearDamping);
        rb.angularVelocity *= (1f - angularDamping);
    }

    private AudioSource AddLoopingAudioSource(float volume)
    {
        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.loop = true;
        source.playOnAwake = false;
        source.volume = volume;
        return source;
    }

    private void PlayAudioSource(AudioSource source, AudioClip clip)
    {
        if (clip == null)
            return;

        if (!source.isPlaying || source.clip != clip)
        {
            source.clip = clip;
            source.Play();
        }
    }

    private void StopAllAudio()
    {
        engineSource.Stop();
        thrustSource.Stop();
        rotationSource.Stop();
    }

    private void PlayParticle(ParticleSystem ps)
    {
        if (ps != null && !ps.isPlaying)
            ps.Play();
    }

    private void StopParticle(ParticleSystem ps)
    {
        if (ps != null && ps.isPlaying)
            ps.Stop();
    }

    private void StopAllParticles()
    {
        StopParticle(mainThrustParticleSystem);
        StopParticle(leftThrustParticleSystem);
        StopParticle(rightThrustParticleSystem);
    }
}