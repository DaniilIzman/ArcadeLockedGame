using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Collections;

public class CollisionDetectorGG : MonoBehaviour
{
    [Header("Scene Delays")]
    [SerializeField] float respawnDelay = 1f;
    [SerializeField] float finishDelay = 2f;

    [Header("Respawn Offset")]
    [SerializeField] float respawnHeightOffset = 2f;

    [Header("Score")]
    [SerializeField] int finishBonusPoints = 500;

    [Header("Audio")]
    [SerializeField] AudioClip victoryAudio;
    [SerializeField] AudioClip crashAudio;

    [Header("Particles")]
    [SerializeField] ParticleSystem crashParticleSystem;
    [SerializeField] ParticleSystem victoryParticleSystem;

    AudioSource audioSource;
    MovementGG movement;
    HealthSystemGG health;
    ScoreSystemGG score;
    Rigidbody rb;
    Vector3 levelStartPosition;
    Quaternion levelStartRotation;
    bool isControllable = true;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        movement = GetComponent<MovementGG>();
        health = GetComponent<HealthSystemGG>();
        score = GetComponent<ScoreSystemGG>();
        rb = GetComponent<Rigidbody>();

        if (audioSource == null) Debug.LogError("AudioSource not found!");
        if (movement == null) Debug.LogError("MovementGG not found!");
        if (health == null) Debug.LogError("HealthSystemGG not found!");
        if (score == null) Debug.LogError("ScoreSystemGG not found!");
        if (rb == null) Debug.LogError("Rigidbody not found!");

        levelStartPosition = transform.position;
        levelStartRotation = transform.rotation;
        
        Debug.Log("🎮 Level started at position: " + levelStartPosition);
    }

    void OnCollisionEnter(Collision other)
    {
        if (!isControllable)
            return;

        if (other.gameObject.CompareTag("Finish"))
        {
            OnGameFinish();
        }
        else if (other.gameObject.CompareTag("Spawn") || other.gameObject.CompareTag("Checkpoint"))
        {
            return;
        }
        else
        {
            OnGameCrash();
        }
    }

    void OnGameCrash()
    {
        isControllable = false;
        movement.enabled = false;
        
        audioSource.Stop();
        audioSource.PlayOneShot(crashAudio);
        crashParticleSystem.Play();

        bool hasLivesLeft = health.TakeDamage();

        if (hasLivesLeft)
        {
            Debug.Log("Crashed! Lives left: " + health.GetCurrentLives());
            StartCoroutine(RespawnCoroutine());
        }
        else
        {
            Debug.Log("Game Over! Final Score: " + score.GetCurrentScore());
            StartCoroutine(GameOverCoroutine());
        }
    }

    void OnGameFinish()
    {
        isControllable = false;
        movement.enabled = false;
        
        score.AddScore(finishBonusPoints);
        
        audioSource.Stop();
        audioSource.PlayOneShot(victoryAudio);
        victoryParticleSystem.Play();

        Debug.Log("Level finished! Final Score: " + score.GetCurrentScore());
        StartCoroutine(FinishLevelCoroutine());
    }

    IEnumerator RespawnCoroutine()
    {
        yield return new WaitForSeconds(respawnDelay);

        Vector3 respawnPos = levelStartPosition;
        bool isCheckpointRespawn = false;

        if (CheckpointManagerGG.instance != null && CheckpointManagerGG.instance.HasCheckpointBeenReached())
        {
            respawnPos = CheckpointManagerGG.instance.GetLastCheckpointPosition();
            isCheckpointRespawn = true;
        }

        if (isCheckpointRespawn)
        {
            respawnPos.y += respawnHeightOffset;
        }

        Quaternion respawnRot = levelStartRotation;

        Debug.Log("Respawned at: " + respawnPos);

        transform.position = respawnPos;
        transform.rotation = respawnRot;
        rb.linearVelocity = Vector3.zero;

        isControllable = true;
        movement.enabled = true;
    }

    IEnumerator GameOverCoroutine()
    {
        yield return new WaitForSeconds(respawnDelay);
        ReturnToArcadeRoom();
    }

    IEnumerator FinishLevelCoroutine()
    {
        yield return new WaitForSeconds(finishDelay);
        ReturnToArcadeRoom();
    }

    void ReturnToArcadeRoom()
    {
        Debug.Log("RETURNING TO ARCADE ROOM");
        Debug.Log("Final Score: " + score.GetCurrentScore());
        
        if (PlayerCreditsAMI.instance == null)
        {
            Debug.LogError("❌ PlayerCreditsAMI not found!");
            SceneManager.LoadScene("ArcadeRoom");
            return;
        }
        
        CreditCalculatorGG creditCalc = GetComponent<CreditCalculatorGG>();
        if (creditCalc != null)
        {
            int creditsEarned = creditCalc.CalculateCredits(score.GetCurrentScore());
            Debug.Log("💳 Credits earned: " + creditsEarned);
            PlayerCreditsAMI.instance.AddCredits(creditsEarned);
        }
        else
        {
            Debug.LogError("CreditCalculatorGG not found on player!");
        }
        
        if (CheckpointManagerGG.instance != null)
        {
            CheckpointManagerGG.instance.ResetCheckpoints();
        }
        
        SceneManager.LoadScene("ArcadeRoom");
    }
}