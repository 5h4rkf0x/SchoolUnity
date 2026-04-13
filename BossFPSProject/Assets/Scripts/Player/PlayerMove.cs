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
        AnimationControl();
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

        float currentSpeed = isSprinting ? sprintSpeed : moveSpeed;

        // rigid.linearVelocity = moveDir * currentSpeed;
        Vector3 velocity = rigid.linearVelocity;
        velocity.x = moveDir.x * currentSpeed;
        velocity.z = moveDir.z * currentSpeed;
        rigid.linearVelocity = velocity;

    }

    private void AnimationControl()
    {
        Vector3 vector = new Vector3(rigid.linearVelocity.x, 0f, rigid.linearVelocity.z);
        float Velocity= vector.magnitude / sprintSpeed;
        Velocity = Mathf.Clamp01(Velocity);

        animator.SetFloat("Velocity", Velocity);
        animator.SetFloat("DirX", moveInput.x);
        animator.SetFloat("DirY", moveInput.y);
    }
}
