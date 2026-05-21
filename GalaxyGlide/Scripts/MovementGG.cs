using UnityEngine;
using UnityEngine.InputSystem;

public class MovementGG : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] InputAction up;
    [SerializeField] InputAction rotation;

    [Header("Movement Settings")]
    [SerializeField] float thrustForce = 0f;
    [SerializeField] float rotationForce = 0f;

    [Header("Audio")]
    [SerializeField] AudioClip thrustAudio;

    [Header("Particles")]
    [SerializeField] ParticleSystem mainThrustParticleSystem;
    [SerializeField] ParticleSystem leftThrustParticleSystem;
    [SerializeField] ParticleSystem rightThrustParticleSystem;

    Rigidbody myRigidBody;
    AudioSource audioSource;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
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
    }

    void FixedUpdate()
    {
        ThrustController();
        RotationController();
    }

    void ThrustController()
    {
        if (up.IsPressed())
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

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(thrustAudio);
        }

        if (!mainThrustParticleSystem.isPlaying)
        {
            mainThrustParticleSystem.Play();
        }
    }

    void StopThrusting()
    {
        audioSource.Stop();

        mainThrustParticleSystem.Stop();
    }

    void RotationController()
    {
        float rotationInput = rotation.ReadValue<float>();

        if (rotationInput < 0)
        {
            RightRotation();
        }
        else if (rotationInput > 0)
        {
            LeftRotation();
        }
        else
        {
            StopRotation();
        }
    }

    void RightRotation()
    {
        ApplyRotation(rotationForce);

        if (!rightThrustParticleSystem.isPlaying)
        {
            leftThrustParticleSystem.Stop();

            rightThrustParticleSystem.Play();
        }
    }

    void LeftRotation()
    {
        ApplyRotation(-rotationForce);

        if (!leftThrustParticleSystem.isPlaying)
        {
            rightThrustParticleSystem.Stop();

            leftThrustParticleSystem.Play();
        }
    }

    void StopRotation()
    {
        leftThrustParticleSystem.Stop();

        rightThrustParticleSystem.Stop();
    }

    void ApplyRotation(float rotationPerFrame)
    {
        myRigidBody.freezeRotation = true;

        transform.Rotate(Vector3.forward * rotationPerFrame * Time.fixedDeltaTime);

        myRigidBody.freezeRotation = false;
    }
}