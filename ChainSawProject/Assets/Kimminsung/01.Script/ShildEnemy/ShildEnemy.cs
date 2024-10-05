using System.Collections;
using UnityEngine;

public class ShieldEnemy : MonoBehaviour
{
    public float health = 25f; // 적 체력
    public float shieldHealth = 200f; // 방패 체력
    public float damage = 10f; // 공격 데미지
    public float dashSpeed = 5f; // 돌진 속도
    public float dashDistance = 3f; // 돌진 거리
    public float invulnerableTime = 3f; // 무적 시간
    public float chaseSpeed = 2f; // 플레이어 추적 속도
    public float chaseRange = 10f; // 플레이어를 추적할 범위
    public float dashRange = 3f; // 돌진 공격을 실행할 범위
    public GameObject deathEffect; // 사망 이펙트 파티클 시스템

    private bool isDashing = false;
    private bool isInvulnerable = false;

    private Transform player; // 플레이어의 위치를 추적하기 위한 변수
    private Rigidbody2D rb; // Rigidbody2D 참조
    private Animator animator; // 애니메이터 참조

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D 컴포넌트 가져오기
        rb.constraints = RigidbodyConstraints2D.FreezePositionY; // Y축 움직임 고정
        player = GameObject.FindGameObjectWithTag("KPlayer").transform; // 플레이어 오브젝트 찾기
        animator = GetComponent<Animator>(); // Animator 컴포넌트 가져오기
    }

    void Update()
    {
        if (health > 0 && player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            // 플레이어가 추적 범위 내에 있을 때
            if (distanceToPlayer < chaseRange && distanceToPlayer > dashRange)
            {
                ChasePlayer();
                animator.SetBool("ShieldRun", true); // 걷는 애니메이션 재생
                animator.SetBool("ShieldIdle", false); // 대기 상태는 끔
            }
            // 추적 범위를 벗어났을 때
            else
            {
                rb.velocity = Vector2.zero; // 멈춤
                animator.SetBool("ShieldRun", false); // 걷기 애니메이션 끔
                animator.SetBool("ShieldIdle", true); // 대기 애니메이션 재생
            }

            // 플레이어가 돌진 공격 범위 내에 있을 때
            if (distanceToPlayer <= dashRange && !isDashing)
            {
                StartCoroutine(DashAttack());
            }

            // 방패 방어 무적 패턴
            if (Input.GetKeyDown(KeyCode.LeftShift)) // 방패 방어 트리거
            {
                StartCoroutine(ActivateInvulnerability());
            }

            // 적이 죽었을 때
            if (health <= 0)
            {
                Die();
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

    // 돌진 공격 패턴
    IEnumerator DashAttack()
    {
        if (!isDashing)
        {
            isDashing = true;
            Debug.Log("돌진 공격 시작"); // 돌진 전 디버그 메시지

            Vector2 dashDirection = (player.position - transform.position).normalized; // 플레이어 쪽으로 돌진
            Vector2 targetPosition = (Vector2)transform.position + dashDirection * dashDistance; // 돌진 목표 위치 계산

            // 돌진하는 동안 바로 목표 위치로 이동
            rb.MovePosition(targetPosition);

            // 플레이어에게 피해를 주기
            Player playerScript = player.GetComponent<Player>(); // 플레이어 스크립트 참조
            if (playerScript != null)
            {
                playerScript.TakeHit(damage, transform.position); // 플레이어에게 데미지 전달
            }
            

            // 돌진 후 잠깐의 대기시간 (2초 동안 멈춤)
            rb.velocity = Vector2.zero; // 속도 0으로 설정하여 멈추게 함
            yield return new WaitForSeconds(2f); // 2초 동안 멈춤

            isDashing = false; // 돌진 완료 후 다시 돌진 가능하게 설정
        }
    }

    // 방패 무적 상태 활성화
    IEnumerator ActivateInvulnerability()
    {
        if (!isInvulnerable)
        {
            isInvulnerable = true;
            yield return new WaitForSeconds(invulnerableTime);
            isInvulnerable = false;
        }
    }

    // 적이 죽는 함수
    void Die()
    {
        animator.SetBool("ShieldDie", true); // 죽는 애니메이션 재생
        rb.velocity = Vector2.zero; // 멈추게 함

        // 사망 이펙트 파티클 시스템 생성
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }

        Destroy(gameObject, 2f); // 2초 후 오브젝트 제거
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

            // 적이 죽었을 경우
            if (health <= 0)
            {
                Die();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("KPlayer"))
        {
            Player player = other.GetComponent<Player>(); // Player 스크립트 참조

            if (player != null)
            {
                player.TakeHit(damage, transform.position); // 플레이어에게 피해 전달
            }

            // 총알 삭제
            Destroy(gameObject);
        }
    }
}
