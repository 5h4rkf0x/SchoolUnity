using UnityEngine;

public class CreditController : MonoBehaviour
{
    [SerializeField] private float speed = 100f;

    private void Awake()
    {
        gameObject.transform.localPosition = new Vector3 (0f, -870f, 0f);
    }

    void Update()
    {
        if (gameObject.transform.localPosition.y >= 1820f) return;
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
}
