using UnityEngine;

public class MovingButton : MonoBehaviour
{
    [SerializeField] private float speed = 150f;
    [SerializeField] private float movingArea = 40f;

    private float currentSpeed;

    private void Awake()
    {
        transform.localPosition = Vector3.zero;

        currentSpeed = speed;
    }

    void Update()
    {
        if (transform.localPosition.y >= (movingArea / 2))
        {
            currentSpeed = -speed;
        }
        else if (transform.localPosition.y <= -(movingArea / 2))
        {
            currentSpeed = speed;
        }

        transform.Translate(Vector3.up * currentSpeed * Time.deltaTime);
    }
}