using UnityEngine;

public class GunAligner : MonoBehaviour
{
    [SerializeField] private Transform stockPoint;   // 개머리판 위치
    [SerializeField] private Transform muzzlePoint;  // 총구 끝 위치
    [SerializeField] private Transform gun;          // 총 오브젝트

    [SerializeField] private Vector3 rotationOffset; // 모델 축 보정

    void LateUpdate()
    {
        Vector3 A = stockPoint.position;
        Vector3 B = muzzlePoint.position;

        Vector3 center = (A + B) * 0.5f;   // 중간 위치
        Vector3 dir = (B - A).normalized;  // 방향

        gun.position = center;

        gun.rotation = Quaternion.LookRotation(dir, Vector3.up)
                     * Quaternion.Euler(rotationOffset);
    }
}