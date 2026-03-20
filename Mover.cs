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
        float xValue = Input.GetAxis("Horizontal");
        float yValue = Input.GetAxis("Vertical");
        float zValue = 0f;
        transform.Translate(xValue, yValue, zValue);
    }
}
