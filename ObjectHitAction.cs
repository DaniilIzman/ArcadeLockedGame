using UnityEngine;

public class ObjectHitAction : MonoBehaviour
{
    [SerializeField] Color ColorHit;
    [SerializeField] bool ResetAfterTimer;
    [SerializeField] float ResetTimer = 0f;
    bool ObjectTouched;
    Color defaultColor;

    void Start()
    {
        defaultColor = GetComponent<MeshRenderer>().material.color;
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
        if(other.gameObject.tag == "Player")
        {
            ObjectTouched = true;
            GetComponent<MeshRenderer>().material.color = ColorHit;
            gameObject.tag = "Hit";
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
            }
        }
    }
}
