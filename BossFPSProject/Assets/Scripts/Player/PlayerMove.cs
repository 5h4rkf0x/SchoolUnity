using JetBrains.Rider.Unity.Editor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    // ≈¨∑°Ω∫ ============================================================================
    [Header("Components")]
    // Move
    [SerializeField] private CapsuleCollider movecoll;
    [SerializeField] private Rigidbody rigid;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Transform playerCam;
    [SerializeField] private Animator animator;

    // GunControl
    [SerializeField] WeaponManager weaponManager;

    [Header("Structs")]
    // Jump
    [SerializeField] private LayerMask groundLayer;

    // Move
    private Vector2 moveInput;
    private Vector3 moveDir;

    [Header("Variable")]
    // Jump
    [SerializeField] private float jumpForce;
    private bool isGrounded;

    // Move
    [SerializeField] private float moveSpeed;
    [SerializeField] private float sprintSpeed;
    private bool isSprinting;

    public CapsuleCollider Movecoll => movecoll;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        weaponManager = GetComponentInChildren<WeaponManager>();
        movecoll = GetComponent<CapsuleCollider>();
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
        float colllocate = movecoll.bounds.min.y + movecoll.radius - 0.2f;
        Vector3 vector3 = new Vector3(movecoll.bounds.min.x, colllocate, movecoll.bounds.min.z);
        isGrounded = Physics.CheckSphere(vector3, movecoll.radius , groundLayer);
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
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

    public void OnReload(InputValue value)
    {
        if (!value.isPressed) return;
        Debug.Log($"{gameObject.name} ¿Á¿Â¿¸");
        weaponManager.Reload();
    }

    public void OnAttack(InputValue value)
    {
        if (value.isPressed)
        {
            weaponManager.StartFire();
        }
        else
        {
            Debug.Log("ø¨ªÁ ∏ÿ√„");
            weaponManager.StopFire();
        }
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

    private void AnimationControl()
    {
        Vector3 vector = new Vector3(rigid.linearVelocity.x, 0f, rigid.linearVelocity.z);
        float Velocity= vector.magnitude / sprintSpeed;
        Velocity = Mathf.Clamp01(Velocity);

        animator.SetFloat("Velocity", Velocity);
        animator.SetFloat("DirX", moveInput.x);
        animator.SetFloat("DirY", moveInput.y);

        animator.SetBool("IsJumping", !isGrounded);
        // if (weaponManager.isReloading)
        // {
        //     animator.SetBool("IsReloading", weaponManager.isReloading);
        // }
    }
}
