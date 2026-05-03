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
            myRigidBody.AddRelativeForce(Vector3.up * thrustForce * Time.fixedDeltaTime);
            if(!AudioSource.isPlaying)
            {
                AudioSource.PlayOneShot(thrustAudio);
            }
            MainThrustParticleSystem.Play();
            LeftThrustParticleSystem.Play();
            RightThrustParticleSystem.Play();
        }
        else
        {
            AudioSource.Stop();
            MainThrustParticleSystem.Stop();
            LeftThrustParticleSystem.Stop();
            RightThrustParticleSystem.Stop();
        }
    }

    private void RotationController()
    {
        float RotationInput = Rotation.ReadValue<float>();
        if(RotationInput < 0)
        {
            ApplyRotation(rotationForce);
        }
        else if(RotationInput > 0)
        {
            ApplyRotation(-rotationForce);
        }
    }

    private void ApplyRotation(float rotationPerFrame)
    {
        myRigidBody.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationPerFrame * Time.fixedDeltaTime);
        myRigidBody.freezeRotation = false;
    }
}
