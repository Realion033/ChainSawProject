using System.Collections;
using UnityEngine;

public class RoyalEnemy : MonoBehaviour
{
    public float health = 100f; // 적 체력
    public float damage = 14f; // 공격 데미지
    public float jumpHeight = 5f; // 점프 높이
    public float fallSpeed = 10f; // 낙하 속도
    public float attackCooldown = 7f; // 공격 쿨타임
    public float attackRange = 5f; // 공격 범위
    private Transform player; // 플레이어의 위치를 추적하기 위한 변수
    public Animator animator; // 애니메이터 컴포넌트 참조
    public ParticleSystem deathParticles; // 죽을 때 사용할 파티클 시스템

    private bool isAttacking = false;
    private bool isAlive = true;

    private Rigidbody2D rb; // Rigidbody2D 참조
    private float nextAttackTime = 0f; // 다음 공격까지 남은 시간

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("KPlayer").transform;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isAlive) return; // 적이 죽으면 아무것도 안 함

        if (health <= 0)
        {
            Die();
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // 플레이어를 추적하는 중일 때
        if (!isAttacking && Time.time >= nextAttackTime)
        {
            if (distanceToPlayer > attackRange)
            {
                ChasePlayer();
            }
            else if (distanceToPlayer <= attackRange)
            {
                StartCoroutine(PerformJumpAttack());
            }
        }
    }

    // 플레이어를 추적하는 함수
    void ChasePlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * 3f; // 추적 속도는 3
        animator.SetBool("RoyalRun", true); // RoyalRun 애니메이션 실행
        animator.SetBool("RoyalIdle", false); // RoyalIdle 비활성화
    }

    // 점프 공격 패턴
    IEnumerator PerformJumpAttack()
    {
        isAttacking = true;
        animator.SetBool("RoyalRun", false); // RoyalRun 멈춤
        animator.SetBool("RoyalIdle", true); // RoyalIdle 대기 애니메이션 실행

        // 위로 점프
        rb.velocity = new Vector2(0, jumpHeight);
        yield return new WaitForSeconds(0.5f); // 점프 후 대기 시간

        // 대각선 아래로 빠르게 낙하하며 공격
        Vector2 attackDirection = (player.position - transform.position).normalized;
        rb.velocity = new Vector2(attackDirection.x, -1) * fallSpeed;

        yield return new WaitForSeconds(0.3f); // 공격 후 대기

        rb.velocity = Vector2.zero; // 속도 0으로 설정하여 공격 후 멈춤
        nextAttackTime = Time.time + attackCooldown; // 공격 쿨타임 설정
        isAttacking = false; // 공격 종료
    }

    // 적이 죽었을 때 호출되는 함수
    void Die()
    {
        isAlive = false;
        animator.SetBool("RoyalDie", true); // 죽는 애니메이션 실행
        animator.SetBool("RoyalRun", false);
        animator.SetBool("RoyalIdle", false);

        // 파티클 시스템 실행
        if (deathParticles != null)
        {
            Instantiate(deathParticles, transform.position, Quaternion.identity);
        }

        Destroy(gameObject, 2f); // 2초 후 오브젝트 삭제
    }

    // 플레이어와 충돌 시 처리하는 함수
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player playerScript = collision.gameObject.GetComponent<Player>();

            if (playerScript != null)
            {
                Debug.Log("Hit");
            }
        }
    }

    // 적이 피해를 받는 함수
    public void TakeDamage(float amount)
    {
        health -= amount;

        // 적이 죽었을 때 처리
        if (health <= 0)
        {
            Die();
        }
    }
}
