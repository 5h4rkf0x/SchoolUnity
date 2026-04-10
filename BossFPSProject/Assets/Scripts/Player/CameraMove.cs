using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private Transform playerBody;
    [SerializeField] private Transform cameraPivot;
    [SerializeField] private InputActionReference lookAction;

    [SerializeField] private float mouseSensitivity = 0.1f;
    [SerializeField] private float minPitch = -30f;
    [SerializeField] private float maxPitch = 60f;

    private float pitch;

    private void OnEnable()
    {
        if (lookAction != null && lookAction.action != null)
        {
            lookAction.action.Enable();
        }
        else
        {
            Debug.LogError("lookAction이 연결되지 않았습니다.");
        }
    }

    private void OnDisable()
    {
        if (lookAction != null && lookAction.action != null)
        {
            lookAction.action.Disable();
        }
    }

    private void LateUpdate()
    {
        if (lookAction == null || lookAction.action == null)
        {
            return;
        }

        if (playerBody == null)
        {
            Debug.LogError("playerBody가 연결되지 않았습니다.");
            return;
        }

        if (cameraPivot == null)
        {
            Debug.LogError("cameraPivot이 연결되지 않았습니다.");
            return;
        }

        Vector2 lookInput = lookAction.action.ReadValue<Vector2>();

        float mouseX = lookInput.x * mouseSensitivity;
        float mouseY = lookInput.y * mouseSensitivity;

        playerBody.Rotate(Vector3.up * mouseX);

        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        cameraPivot.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }
}