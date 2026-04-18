using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField] float UpValue = 0f;
    [SerializeField] float ActivationTimer = 0f;
    [SerializeField] float SpikedTimer = 0f;
    float timer = 0f;
    float SaveInitialTimer = 0f;
    bool IsUp = false;
    Transform SpikePosition;
    Vector3 InitialPosition;
    Vector3 upPosition;
    void Start()
    {
        InitialPosition = SpikePosition.transform.position;
        upPosition = InitialPosition + new Vector3(0f, UpValue, 0f);
    }

    void Update()
    {
        SpikesBehaviour();
    }
    void SpikesBehaviour()
    {
        timer += Time.deltaTime;
        if(!IsUp && timer >= ActivationTimer)
        {
            transform.position = upPosition;
            IsUp = true;
            timer = 0f;
        }
        else if(IsUp && timer >= SpikedTimer)
        {
            transform.position = InitialPosition;
            IsUp = false;
            timer = 0f;
        }
    }
}
