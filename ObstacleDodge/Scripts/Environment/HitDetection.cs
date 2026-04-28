using UnityEngine;

public class HitDetection : MonoBehaviour
{
    [SerializeField] Color ColorHit;
    [SerializeField] bool ResetAfterTimer;
    public float ResetTimer = 0f;
    public bool ObjectTouched;
    Color defaultColor;
    float initialTime = 0f;
    string initialTag;
    [SerializeField] bool DestroyUponHit;
    void Start()
    {
        defaultColor = GetComponent<MeshRenderer>().material.color;
        initialTime = ResetTimer;
        initialTag = gameObject.tag;
    }
    void Update()
    {
        if(ResetAfterTimer == true && ObjectTouched == true)
        {
            ResetToDefault();
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            ObjectTouched = true;
            GetComponent<MeshRenderer>().material.color = ColorHit;
            gameObject.tag = "Hit";
            if(DestroyUponHit)
            {
                Destroy(gameObject);
            }
        }
    }
    void ResetToDefault()
    {
        if(ResetAfterTimer == true && ResetTimer >= 0f)
        {
            ResetTimer -= Time.deltaTime;
            if(ResetTimer <= 0f)
            {
                GetComponent<MeshRenderer>().material.color = defaultColor;
                ObjectTouched = false;
                ResetTimer = initialTime;
                gameObject.tag = initialTag;
            }
        }
    }
}
