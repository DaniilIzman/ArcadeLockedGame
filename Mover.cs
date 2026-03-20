using UnityEngine;

public class Mover : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        [SerializeField] float xValue = 0f;
        [SerializeField] float yValue = 0.01f;
        [SerializeField] float zValue = 0f;
        transform.Translate(xValue, yValue, zValue);
    }
}
