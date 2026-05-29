using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerPositionRestorer : MonoBehaviour
{
    void Start()
    {
        RestorePositionIfAvailable();
    }

    void RestorePositionIfAvailable()
    {
        if (PlayerPositionManager.instance == null || !PlayerPositionManager.instance.HasData())
        {
            Debug.Log("[PlayerPositionRestorer] No saved position — keeping spawn position.");
            return;
        }

        Vector3    savedPos = PlayerPositionManager.instance.GetSavedPosition();
        Quaternion savedRot = PlayerPositionManager.instance.GetSavedRotation();

        Rigidbody rb = GetComponent<Rigidbody>();

        rb.constraints = RigidbodyConstraints.FreezeRotation;

        rb.linearVelocity  = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        rb.MovePosition(savedPos);
        rb.MoveRotation(savedRot);

        transform.position = savedPos;
        transform.rotation = savedRot;

        HumanMovementAMI movement = GetComponent<HumanMovementAMI>();
        if (movement != null) movement.enabled = true;

        HumanCameraAMI camera = GetComponentInChildren<HumanCameraAMI>();
        if (camera != null) camera.enabled = true;

        Debug.Log($"[PlayerPositionRestorer] Restored player to {savedPos}");

        PlayerPositionManager.instance.ClearData();
    }
}