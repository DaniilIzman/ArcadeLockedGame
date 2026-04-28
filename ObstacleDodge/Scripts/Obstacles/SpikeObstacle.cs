using UnityEngine;

public class SpikeObstacle : MonoBehaviour
{
    [SerializeField] float UpValue = 0f;
    [SerializeField] float ActivationTimer = 0f;
    [SerializeField] float SpikedTimer = 0f;
    float TimerBeforeActivation = 0f;
    float timer = 0f;

    bool IsUp = false;
    
    Vector3 InitialPosition;
    Vector3 upPosition;
    
    TriggerHazard Trigger;
    MeshRenderer SpikeRenderer;

    void Awake()
    {
        Trigger = FindAnyObjectByType<TriggerHazard>();
        if(Trigger.GameObjectList.Contains(gameObject))
        {
            gameObject.SetActive(false); 
        }
    }
    void Start()
    {
        InitialPosition = transform.position;
        upPosition = InitialPosition + new Vector3(0f, UpValue, 0f);
        SpikeRenderer = GetComponent<MeshRenderer>();
        TriggerHazard[] triggers1 = FindObjectsByType<TriggerHazard>(FindObjectsSortMode.None);
        foreach (TriggerHazard trigger in triggers1)
        {
            if (trigger.GameObjectList.Contains(gameObject))
            {
                gameObject.SetActive(false);
                break;
            }
        }
        SpikeRenderer.enabled = false;
    }

    void OnEnable()
    {
        TimerBeforeActivation = Time.time;
    }

    void Update()
    {
        SpikesBehaviour();
    }

    void SpikesBehaviour()
    {
        timer += Time.deltaTime;

        if(!IsUp && Time.time >= ActivationTimer + TimerBeforeActivation)
        {
            SpikeRenderer.enabled = true;
            transform.position = upPosition;
            IsUp = true;
            TimerBeforeActivation = Time.time;
        }

        else if(IsUp && Time.time >= SpikedTimer + TimerBeforeActivation)
        {
            transform.position = InitialPosition;
            IsUp = false;
            TimerBeforeActivation = Time.time;
        }
    }
}
