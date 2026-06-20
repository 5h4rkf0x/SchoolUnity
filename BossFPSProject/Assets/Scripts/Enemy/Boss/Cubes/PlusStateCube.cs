using UnityEngine;

public class PlusStateCube : MonoBehaviour
{

    // 보스 이동은 잘 되는데 이후 바로 즉사함

    [SerializeField] private CubeManager cubeManager;
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cubeManager = FindFirstObjectByType<CubeManager>();
    }



    private void FixedUpdate()
    {
        Move(cubeManager.cubeSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.tag == "Player" && Player.instance.CurrentPlayerState == Player.PlayerStates.Plus)
        {
            gameObject.SetActive(false);
        }
        else if (other.tag == "Boss")
        {
            Player.instance.TakeDamage(100);
            gameObject.SetActive(false);
        }
    }

    private void Move(float speed)
    {
        Vector3 dir = cubeManager.transform.position - transform.position;
        dir.Normalize();
        rb.linearVelocity = dir * speed;
    }
}
