using UnityEngine;

public class Gun : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip shoot;
    public GameObject laserPrefab; // 발사할 레이저 프리팹
    public Transform firePoint; // 발사 위치
    public float laserSpeed = 10f; // 레이저 속도
    public float minFireRate = 0.5f; // 최소 발사 속도
    public float maxFireRate = 2f; // 최대 발사 속도
    public float fireRange = 15f; // 발사 범위
    public LayerMask targetLayer; // 타겟을 감지할 레이어
    private float nextFireTime; // 다음 발사 시간
    private Collider2D target; // 현재 감지된 타겟

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        // 발사 범위 내에 있는 적 탐색 (오버랩 감지)
        target = Physics2D.OverlapCircle(firePoint.position, fireRange, targetLayer);

        // 타겟이 범위 안에 있고, 발사 시간이 되었을 때 발사
        if (target != null && Time.time >= nextFireTime)
        {
            FireLaser();
            // 발사 후 랜덤한 속도로 다음 발사 시간 설정
            float randomFireRate = Random.Range(minFireRate, maxFireRate);
            nextFireTime = Time.time + 1f / randomFireRate;
        }
    }

    void FireLaser()
    {
        if (laserPrefab && firePoint)
        {
            // 레이저 생성
            audioSource.PlayOneShot(shoot);
            GameObject laser = Instantiate(laserPrefab, firePoint.position, firePoint.rotation);

            // 레이저에 Rigidbody2D 추가 및 속도 적용
            Rigidbody2D rb = laser.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = firePoint.right * laserSpeed; // 레이저를 발사 지점의 방향으로 이동
            }

            // 레이저가 20초 후에 사라지도록 설정
            Destroy(laser, 10f);
        }
    }

    // 기지모로 발사 범위 시각화
    private void OnDrawGizmosSelected()
    {
        // 발사 범위를 표시 (기지모 색상 설정)
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(firePoint.position, fireRange);
    }
}
