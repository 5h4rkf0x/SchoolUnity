using JetBrains.Rider.Unity.Editor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class PlayerMove : MonoBehaviour
{
    // ≈¨∑°Ω∫ ============================================================================
    [Header("Components")]
    // Move
    [SerializeField] private CapsuleCollider movecoll;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Transform playerCam;
    [SerializeField] private PlayerAnimation playerAnimation;
    [SerializeField] private AudioClip jumpClip;


    // GunControl
    [SerializeField] WeaponManager weaponManager;
    [SerializeField] private MagazineManager magazineManager;

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
        rb = GetComponent<Rigidbody>();
        playerAnimation = GetComponentInChildren<PlayerAnimation>();
        weaponManager = GetComponentInChildren<WeaponManager>();
        magazineManager = GetComponentInChildren<MagazineManager>();
        movecoll = GetComponent<CapsuleCollider>();
        playerInput.camera = Camera.main;
    }

    private void Update()
    {
        playerAnimation.AnimationControl(rb, sprintSpeed, moveInput, isGrounded);
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
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

            AudioManager.instance.PlaySFX(jumpClip);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
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

    public void OnChangeState(InputValue value)
    {
        if (value.isPressed)
        {
            Player.instance.ChangeState();
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

        Vector3 velocity = rb.linearVelocity;
        velocity.x = moveDir.x * currentSpeed;
        velocity.z = moveDir.z * currentSpeed;
        rb.linearVelocity = velocity;
    }
}
