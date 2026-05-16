using UnityEngine;

public class PlayerAliner : MonoBehaviour
{
    [SerializeField] private Camera mainCam;
    [SerializeField] private MagazineManager magazineManager;

    [SerializeField] private float modelYawOffset = 45f;

    private void Awake()
    {
        Transform playerOverall = transform.root;     // 여기 아래부터 KnifeManager로 이동
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
        Vector3 dir = mainCam.transform.forward;

        // 위아래 회전 제거
        dir.y = 0f;

        // 방향이 너무 작으면 회전 안함
        if (dir.sqrMagnitude < 0.001f) return;

        // 목표 회전
        Quaternion targetRot = Quaternion.LookRotation(dir.normalized) * Quaternion.Euler(0f, modelYawOffset, 0f);
        transform.rotation = targetRot;
    }
}