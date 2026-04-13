using JetBrains.Rider.Unity.Editor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    // Move
    [SerializeField] private float moveSpeed;
    [SerializeField] private float sprintSpeed;
    
    private Vector2 moveInput;
    private Vector3 moveDir;
    private bool isSprinting;

    // Jump
    [SerializeField] private float jumpForce;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask groundLayer;

    private bool isGrounded;


    [SerializeField] private Rigidbody rigid;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Transform playerCam;
    [SerializeField] private Animator animator;

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
        AnimationControl();
    }

    private void FixedUpdate()
    {
        isSprinting = playerInput.actions["Sprint"].IsPressed();
        Move();
        CheckGround();
    }
    private void CheckGround()
    {
        isGrounded = Physics.CheckSphere( groundCheck.position, groundCheckRadius, groundLayer );
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

        float currentSpeed = isSprinting ? sprintSpeed : moveSpeed;

        Vector3 velocity = rigid.linearVelocity;
        velocity.x = moveDir.x * currentSpeed;
        velocity.z = moveDir.z * currentSpeed;
        rigid.linearVelocity = velocity;
    }

    public void OnJump(InputValue value)
    {
        if (!value.isPressed) return;

        if (isGrounded)
        {
            rigid.linearVelocity = new Vector3(rigid.linearVelocity.x, 0f, rigid.linearVelocity.z);

            rigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void AnimationControl()
    {
        Vector3 vector = new Vector3(rigid.linearVelocity.x, 0f, rigid.linearVelocity.z);
        float Velocity= vector.magnitude / sprintSpeed;
        Velocity = Mathf.Clamp01(Velocity);

        animator.SetFloat("Velocity", Velocity);
        animator.SetFloat("DirX", moveInput.x);
        animator.SetFloat("DirY", moveInput.y);

        animator.SetBool("IsJumping", !isGrounded);
    }
}
