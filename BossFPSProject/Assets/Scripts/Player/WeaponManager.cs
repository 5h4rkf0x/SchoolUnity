using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class WeaponManager: MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Camera cam;
    [SerializeField] private Transform fireTransform;
    [SerializeField] private Rigidbody bulletPrefab;
    [SerializeField] private float bulletSpeed = 40f;
    [SerializeField] private float maxDistance = 100f;

    public bool isReloading = false;

    public void OnAttack(InputValue value)
    {
        Fire();
    }

    public void OnReload(InputValue value)
    {
        if (!value.isPressed) return;
        isReloading = true;
        Debug.Log($"{gameObject.name} ¿Á¿Â¿¸");
    }

    private void Fire()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.origin + ray.direction * maxDistance;
        }

        Vector3 shootDir = (targetPoint - fireTransform.position).normalized;

        Rigidbody bullet = Instantiate(bulletPrefab, fireTransform.position, Quaternion.LookRotation(shootDir));

        bullet.linearVelocity = shootDir * bulletSpeed;
    }
}