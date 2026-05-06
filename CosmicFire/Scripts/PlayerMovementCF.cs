using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovementCF : MonoBehaviour
{
    [SerializeField] float controlSpeed = 0f;
    Vector2 movement;   
    
    void Update()
    {
        Translation();
    }

    public void OnMove(InputValue value)
    {
        movement = value.Get<Vector2>();
    }
    
    private void Translation()
    {
        float xOffset = movement.x * controlSpeed * Time.deltaTime;
        float yOffset = movement.y * controlSpeed * Time.deltaTime;
        transform.localPosition = new Vector3(transform.localPosition.x + xOffset, transform.localPosition.x + yOffset, 0f);
    }
}
