using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField] float UpValue = 0f;
    [SerializeField] float ActivationTimer = 0f;
    [SerializeField] float SpikedTimer = 0f;
    float SaveInitialTimer = 0f;
    void Start()
    {
        SaveInitialTimer = ActivationTimer;
    }

    void Update()
    {
        if(Time.time >= ActivationTimer + SaveInitialTimer)
        transform.Translate(0f, UpValue, 0f);
    }
}
