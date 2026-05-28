using UnityEngine;
using System.Collections;

public class HumanMovementAMI : MonoBehaviour
{
    [Header("Grid Settings")]
    public float moveDistance = 2f;
    public float moveDuration = 0.25f;
    public float turnDuration = 0.2f;
    public LayerMask obstacleLayer;

    public bool isMoving { get; private set; } 
    public bool isCrouching = false;

    void Update()
    {
        if (isMoving) return;

        if (Input.GetKeyDown(KeyCode.W))
            TryMove(transform.forward);
        else if (Input.GetKeyDown(KeyCode.S))
            TryMove(-transform.forward);

        if (Input.GetKeyDown(KeyCode.A))
            StartCoroutine(RotateRoutine(-90f));
        else if (Input.GetKeyDown(KeyCode.D))
            StartCoroutine(RotateRoutine(90f));
    }

    private void TryMove(Vector3 direction)
    {
        Vector3 targetPosition = transform.position + (direction * moveDistance);

        Vector3 rayOrigin = transform.position + Vector3.up * 0.5f;
        
        if (!Physics.Raycast(rayOrigin, direction, moveDistance, obstacleLayer))
        {
            StartCoroutine(MoveRoutine(targetPosition));
        }
        else
        {
            Debug.Log("Path blocked!");
        }
    }

    private IEnumerator MoveRoutine(Vector3 targetPos)
    {
        isMoving = true;
        Vector3 startPos = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;
        isMoving = false;
    }

    private IEnumerator RotateRoutine(float angle)
    {
        isMoving = true;
        Quaternion startRot = transform.rotation;
        Quaternion targetRot = startRot * Quaternion.Euler(0, angle, 0);
        float elapsedTime = 0f;

        while (elapsedTime < turnDuration)
        {
            transform.rotation = Quaternion.Lerp(startRot, targetRot, elapsedTime / turnDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = targetRot;
        isMoving = false;
    }
}