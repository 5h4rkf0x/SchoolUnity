using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float sprintSpeed;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Animator animator;

    private Vector2 moveInput;

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    private void Update()
    {
        bool isSprinting = playerInput.actions["Sprint"].IsPressed();
        bool isWalking = moveInput.sqrMagnitude > 0.01f;

        Vector3 forwardAxis = transform.right;
        Vector3 sideAxis = transform.forward;

        forwardAxis.y = 0f;
        sideAxis.y = 0f;

        forwardAxis.Normalize();
        sideAxis.Normalize();

        Vector3 moveDir = forwardAxis * moveInput.x + sideAxis * moveInput.y;

        if (moveDir.sqrMagnitude > 1f)
            moveDir.Normalize();

        float currentSpeed = moveSpeed;

        if (moveInput.sqrMagnitude > 0.01f && isSprinting)
        {
            currentSpeed = sprintSpeed;
        }

        transform.position += moveDir * currentSpeed * Time.deltaTime;
        animator.SetBool("IsWalking", isWalking);
        animator.SetBool("IsSprinting", isWalking && isSprinting);
    }
}