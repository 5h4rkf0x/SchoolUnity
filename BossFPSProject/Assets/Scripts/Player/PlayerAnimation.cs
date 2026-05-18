using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Animator animator;
    [SerializeField] private MagazineManager magazineManager;

    [SerializeField] private float modelYawOffset = 45f;

    private void Awake()
    {
        cam = Camera.main;
        if (cam == null) Debug.LogWarning("playerAliner.cam == null");

        animator = GetComponent<Animator>();

        Transform playerOverall = transform.root;
        magazineManager = playerOverall.GetComponentInChildren<MagazineManager>();
    }

    private void Update()
    {
        RotatePlayer();
    }

    public void ReloadAnimControll()
    {
        magazineManager.ReloadEndSet();
    }

    private void RotatePlayer()
    {
        // 카메라가 바라보는 방향
        Vector3 dir = cam.transform.forward;

        // 위아래 회전 제거
        dir.y = 0f;

        // 방향이 너무 작으면 회전 안함
        if (dir.sqrMagnitude < 0.001f) return;

        // 목표 회전
        Quaternion targetRot = Quaternion.LookRotation(dir.normalized) * Quaternion.Euler(0f, modelYawOffset, 0f);
        transform.rotation = targetRot;
    }
    
    public void AnimationControl(Rigidbody rb, float sprintSpeed, Vector2 moveInput, bool isGrounded)
    {
        Vector3 vector = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        float Velocity = vector.magnitude / sprintSpeed;
        Velocity = Mathf.Clamp01(Velocity);

        animator.SetFloat("Velocity", Velocity);
        animator.SetFloat("DirX", moveInput.x);
        animator.SetFloat("DirY", moveInput.y);

        animator.SetBool("IsJumping", !isGrounded);
        animator.SetBool("IsReloading", magazineManager.IsReloading);
    }
}