using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovementCF : MonoBehaviour
{
    [SerializeField] float controlSpeed = 0f;

    void Update()
    {
        float xOffset = controlSpeed * Time.deltaTime;
        transform.localPosition = new Vector3(transform.localPosition.x + xOffset, 0f, 0f);
    }

    public void OnMove(InputValue value)
    {
        Debug.Log(value.Get<Vector2>());
    }
}
