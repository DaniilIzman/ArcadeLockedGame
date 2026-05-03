using UnityEngine;
using UnityEngine.InputSystem;

public class MovementGG : MonoBehaviour
{
    [SerializeField] InputAction Up;
    [SerializeField] InputAction Rotation;
    [SerializeField] float thrustForce = 0f;
    [SerializeField] float rotationForce = 0f;
    [SerializeField] AudioClip thrustAudio;
    [SerializeField] ParticleSystem MainThrustParticleSystem;
    [SerializeField] ParticleSystem LeftThrustParticleSystem;
    [SerializeField] ParticleSystem RightThrustParticleSystem;
    Rigidbody myRigidBody;
    AudioSource AudioSource;
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
        AudioSource = GetComponent<AudioSource>();
    }
    void OnEnable()
    {
        Up.Enable();
        Rotation.Enable();
    }

    void FixedUpdate()
    {
        ThrustController();
        RotationController();
    }

    private void ThrustController()
    {
        if (Up.IsPressed())
        {
            BeginThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    private void BeginThrusting()
    {
        myRigidBody.AddRelativeForce(Vector3.up * thrustForce * Time.fixedDeltaTime);
        if (!AudioSource.isPlaying)
        {
            AudioSource.PlayOneShot(thrustAudio);
        }
        if (!MainThrustParticleSystem.isPlaying)
        {
            MainThrustParticleSystem.Play();
        }
    }

    private void StopThrusting()
    {
        AudioSource.Stop();
        MainThrustParticleSystem.Stop();
    }
    
    private void RotationController()
    {
        float RotationInput = Rotation.ReadValue<float>();
        if(RotationInput < 0)
        {
            RightRotation();
        }
        else if(RotationInput > 0)
        {
            LeftRotation();
        }
        else
        {
            StopRotation();
        }
    }

    private void RightRotation()
    {
        ApplyRotation(rotationForce);
        if (!RightThrustParticleSystem.isPlaying)
        {
            LeftThrustParticleSystem.Stop();
            RightThrustParticleSystem.Play();
        }
    }

    private void LeftRotation()
    {
        ApplyRotation(-rotationForce);
        if (!LeftThrustParticleSystem.isPlaying)
        {
            RightThrustParticleSystem.Stop();
            LeftThrustParticleSystem.Play();
        }
    }

    private void StopRotation()
    {
        LeftThrustParticleSystem.Stop();
        RightThrustParticleSystem.Stop();
    }
    
    private void ApplyRotation(float rotationPerFrame)
    {
        myRigidBody.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationPerFrame * Time.fixedDeltaTime);
        myRigidBody.freezeRotation = false;
    }
}
