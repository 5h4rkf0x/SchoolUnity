using JetBrains.Rider.Unity.Editor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float sprintSpeed;
    [SerializeField] private bool isSprinting;

    [SerializeField] private Vector3 moveDir;
    [SerializeField] private Rigidbody rigid;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Transform playerCam;
    [SerializeField] private Animator animator;

    [SerializeField] private Vector2 moveInput;

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        AnimationControll();
    }

    private void FixedUpdate()
    {
        isSprinting = playerInput.actions["Sprint"].IsPressed();
        Move();
    }

    private void Move()
    {

        Vector3 cameraForward = playerCam.forward;
        Vector3 cameraRight = playerCam.right;

        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward.Normalize();
        cameraRight.Normalize();

        moveDir = cameraRight * moveInput.x + cameraForward * moveInput.y;

        float currentSpeed = moveSpeed;

        if (isSprinting)
        {
            currentSpeed = sprintSpeed;
        }

        rigid.linearVelocity = moveDir * currentSpeed;
    }

    private void AnimationControll()
    {

        Vector2 vector = new Vector2(rigid.linearVelocity.x, rigid.linearVelocity.z);
        float Velocity = vector.magnitude / (sprintSpeed);

        //if (isSprinting)
        //{
        //    Velocity = vector.magnitude / (sprintSpeed);
        //}

        animator.SetFloat("Velocity", Velocity);
        animator.SetFloat("DirX", moveInput.x);
        animator.SetFloat("DirY", moveInput.y);
    }
}