using System.Collections;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    public float health = 25f; // 에너미 체력
    public float shieldHealth = 200f; // 방패 체력
    public float damage = 10f; // 공격 데미지
    public float chaseSpeed = 2f; // 플레이어 추적 속도
    public float chaseRange = 10f; // 플레이어를 추적할 범위
    public float stopDistance = 2f; // 일정 거리까지 플레이어 추적 후 멈춤

    private bool isInvulnerable = false;

    private Transform player; // 플레이어의 위치를 추적하기 위한 변수
    private Rigidbody2D rb; // Rigidbody2D 참조
    private Animator animator; // 애니메이터 참조

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D 컴포넌트 가져오기
        player = GameObject.FindGameObjectWithTag("KPlayer").transform; // 플레이어 오브젝트 찾기
        animator = GetComponent<Animator>(); // Animator 컴포넌트 가져오기
    }

    void Update()
    {
        if (health > 0 && player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            // 플레이어가 추적 범위 내에 있을 때만 이동
            if (distanceToPlayer < chaseRange && distanceToPlayer > stopDistance)
            {
                ChasePlayer();
                animator.SetBool("BulletRun", true); // 이동 중인 애니메이션 재생
                animator.SetBool("BulletIdle", false); // 대기 상태는 끔
            }
            else
            {
                rb.velocity = Vector2.zero; // 일정 거리 이상 접근 시 멈춤
                animator.SetBool("BulletRun", false); // 이동 중인 애니메이션 끔
                animator.SetBool("BulletIdle", true); // 대기 상태로 전환
            }

            // 플레이어 사망 처리
            if (health <= 0)
            {
                animator.SetBool("BulletDie", true); // 사망 애니메이션 재생
                Destroy(gameObject, 2f); // 2초 후 오브젝트 제거
            }
        }
        else
        {
            Destroy(gameObject); // 적이 죽으면 오브젝트 제거
        }
    }

    // 플레이어를 추적하는 함수
    void ChasePlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized; // 플레이어 쪽으로 방향 계산
        rb.velocity = direction * chaseSpeed; // 추적 속도만큼 이동
    }

    // 방패 무적 상태 활성화
    IEnumerator ActivateInvulnerability()
    {
        if (!isInvulnerable)
        {
            isInvulnerable = true;
            yield return new WaitForSeconds(3f); // 무적 시간
            isInvulnerable = false;
        }
    }

    // 피해 받는 함수
    public void TakeDamage(float amount)
    {
        if (!isInvulnerable)
        {
            if (shieldHealth > 0)
            {
                shieldHealth -= amount; // 방패 체력 소모
            }
            else
            {
                health -= amount; // 방패 체력이 0이면 실제 체력 소모
            }

            // 적이 죽었을 경우 사망 애니메이션 재생
            if (health <= 0)
            {
                animator.SetBool("BulletDie", true); // 사망 애니메이션 재생
                Destroy(gameObject, 2f); // 2초 후 오브젝트 제거
            }
        }
    }
}
