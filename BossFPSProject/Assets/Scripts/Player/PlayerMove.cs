using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Vector2 moveInput;

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    private void Update()
    {
        Vector3 forwardAxis = transform.right;
        Vector3 sideAxis = transform.forward;

        forwardAxis.y = 0f;
        sideAxis.y = 0f;

        forwardAxis.Normalize();
        sideAxis.Normalize();

        Vector3 moveDir = forwardAxis * moveInput.x + sideAxis * moveInput.y;

        if (moveDir.sqrMagnitude > 1f)
            moveDir.Normalize();

        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }
}