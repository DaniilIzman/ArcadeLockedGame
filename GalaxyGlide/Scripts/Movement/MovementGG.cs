using UnityEngine;
using UnityEngine.InputSystem;

public class MovementGG : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InputAction up;
    [SerializeField] private InputAction rotation;

    [Header("Movement Settings")]
    [SerializeField] private float thrustForce = 500f;
    [SerializeField] private float rotationForce = 100f;

    [Header("Space Feel")]
    [SerializeField] [Range(0f, 1f)] private float linearDamping = 0.02f;
    [SerializeField] [Range(0f, 1f)] private float angularDamping = 0.08f;

    [Header("Audio")]
    [SerializeField] private AudioClip thrustAudio;
    [SerializeField] private AudioClip rotateLeftAudio;
    [SerializeField] private AudioClip rotateRightAudio;

    [Header("Particles")]
    [SerializeField] private ParticleSystem mainThrustParticleSystem;
    [SerializeField] private ParticleSystem leftThrustParticleSystem;
    [SerializeField] private ParticleSystem rightThrustParticleSystem;

    private Rigidbody myRigidBody;
    private AudioSource audioSource;
    private AudioSource rotationAudioSource;

    private bool isThrusting;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        rotationAudioSource = gameObject.AddComponent<AudioSource>();
        rotationAudioSource.loop = false;
        rotationAudioSource.playOnAwake = false;

        if (myRigidBody != null)
        {
            myRigidBody.freezeRotation = true;
        }
    }

    void OnEnable()
    {
        up.Enable();
        rotation.Enable();
    }

    void OnDisable()
    {
        up.Disable();
        rotation.Disable();

        StopThrusting();
        StopRotationSound();
    }

    void FixedUpdate()
    {
        ThrustController();
        RotationController();
        ApplyDamping();
    }

    void ThrustController()
    {
        isThrusting = up.IsPressed();

        if (isThrusting)
        {
            BeginThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    void BeginThrusting()
    {
        myRigidBody.AddRelativeForce(Vector3.up * thrustForce * Time.fixedDeltaTime);

        if (audioSource != null &&
            thrustAudio != null &&
            !audioSource.isPlaying)
        {
            audioSource.clip = thrustAudio;
            audioSource.loop = true;
            audioSource.Play();
        }

        PlayParticle(mainThrustParticleSystem);
        PlayParticle(leftThrustParticleSystem);
        PlayParticle(rightThrustParticleSystem);
    }

    void StopThrusting()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        StopParticle(mainThrustParticleSystem);
        StopParticle(leftThrustParticleSystem);
        StopParticle(rightThrustParticleSystem);
    }

    void RotationController()
    {
        float rotationInput = rotation.ReadValue<float>();

        if (rotationInput < 0f)
        {
            ApplyRotation(rotationForce);

            PlayRotationSound(rotateRightAudio);

            if (isThrusting)
            {
                StopParticle(leftThrustParticleSystem);
                PlayParticle(rightThrustParticleSystem);
            }
        }
        else if (rotationInput > 0f)
        {
            ApplyRotation(-rotationForce);

            PlayRotationSound(rotateLeftAudio);

            if (isThrusting)
            {
                StopParticle(rightThrustParticleSystem);
                PlayParticle(leftThrustParticleSystem);
            }
        }
        else
        {
            StopRotationSound();

            if (isThrusting)
            {
                PlayParticle(leftThrustParticleSystem);
                PlayParticle(rightThrustParticleSystem);
            }
        }
    }

    void ApplyRotation(float rotationPerFrame)
    {
        transform.Rotate(Vector3.forward * rotationPerFrame * Time.fixedDeltaTime);
    }

    void ApplyDamping()
    {
        myRigidBody.linearVelocity *= (1f - linearDamping);
        myRigidBody.angularVelocity *= (1f - angularDamping);
    }

    void PlayRotationSound(AudioClip clip)
    {
        if (rotationAudioSource == null || clip == null)
        {
            return;
        }

        if (!rotationAudioSource.isPlaying ||
            rotationAudioSource.clip != clip)
        {
            rotationAudioSource.clip = clip;
            rotationAudioSource.loop = true;
            rotationAudioSource.Play();
        }
    }

    void StopRotationSound()
    {
        if (rotationAudioSource != null &&
            rotationAudioSource.isPlaying)
        {
            rotationAudioSource.Stop();
        }
    }

    void PlayParticle(ParticleSystem particleSystem)
    {
        if (particleSystem != null &&
            !particleSystem.isPlaying)
        {
            particleSystem.Play();
        }
    }

    void StopParticle(ParticleSystem particleSystem)
    {
        if (particleSystem != null &&
            particleSystem.isPlaying)
        {
            particleSystem.Stop();
        }
    }
}