using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject laserPrefab; // 레이저 프리팹
    public Transform firePoint; // 레이저를 발사할 위치
    public float laserSpeed = 10f; // 레이저 속도
    public float fireRate = 1f; // 발사 속도 (초당 발사 수)
    private float nextFireTime; // 다음 발사 시간

    private void Update()
    {
        if (Time.time >= nextFireTime)
        {
                FireLaser();
                nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void FireLaser()
    {
        if (laserPrefab && firePoint)
        {
            // 레이저 생성
            GameObject laser = Instantiate(laserPrefab, firePoint.position, firePoint.rotation);

            // 레이저의 Rigidbody2D를 가져와 속도를 설정
            Rigidbody2D rb = laser.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = firePoint.right * laserSpeed; // 레이저를 발사 방향으로 이동
            }

            // 레이저의 생명 주기를 설정 (예: 3초 후 삭제)
            Destroy(laser, 3f);
        }
    }
}
