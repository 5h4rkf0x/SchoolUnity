using UnityEngine;

public class MinusStateCube : MonoBehaviour
{

    // 보스 이동은 잘 되는데 이후 바로 즉사함

    [SerializeField] private CubeManager cubeManager;
    private Rigidbody rb;

    private Vector2 dir;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cubeManager = FindFirstObjectByType<CubeManager>();
    }

    private void Start()
    {
        dir = cubeManager.cubeDir;
    }

    private void FixedUpdate()
    {
        Move(cubeManager.cubeSpeed, dir);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && Player.instance.CurrentPlayerState == Player.PlayerStates.Minus)
        {
            gameObject.SetActive(false);
        }
        else
        {
            Player.instance.TakeDamage(100);
            gameObject.SetActive(false);
        }
    }

    private void Move(float speed, Vector3 dir)
    {
        if (dir.sqrMagnitude < 0.001f)
        {
            rb.linearVelocity = Vector3.zero;
            return;
        }

        dir.Normalize();
        rb.linearVelocity = dir * speed;
    }
}
