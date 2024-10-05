using System.Collections;
using UnityEngine;

public class RoyalEnemy : TestEnemy
{
    public float jumpHeight = 5f; // 점프 높이
    public float fallSpeed = 10f; // 낙하 속도
    public float attackCooldown = 7f; // 공격 쿨타임
    public float attackRange = 5f; // 공격 범위
    public float chaseSpeed = 3f; // 추적 속도
    public ParticleSystem deathParticles; // 죽을 때 사용할 파티클 시스템
    public float damage = 10f; // 총알 데미지 설정

    private Animator animator;
    private Transform player; // 플레이어의 위치를 추적하기 위한 변수
    private Rigidbody2D rb; // Rigidbody2D 참조
    private float nextAttackTime = 0f; // 다음 공격까지 남은 시간

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D 컴포넌트 가져오기
        player = GameObject.FindGameObjectWithTag("KPlayer").transform; // 플레이어 오브젝트 찾기
        health = 100f; // RoyalEnemy의 초기 체력
        maxHealth = health; // 최대 체력 설정
        animator = GetComponent<Animator>();
    }


    private void Update()
    {
        if (isDead) return; // 적이 죽으면 아무것도 안 함

        if (health <= 0)
        {
            Die();
            return; // 죽었을 때는 더 이상 진행하지 않음
        }

        float distanceToPlayer = Mathf.Abs(player.position.x - transform.position.x); // X축 거리 계산

        // 플레이어의 위치에서 X 좌표만 사용하고, Y는 현재 적의 Y 위치를 고정
        Vector2 direction = new Vector2(player.position.x - transform.position.x, 0).normalized;

        // 추적 속도만큼 X축으로만 이동 (Y축은 0으로 고정됨)
        rb.velocity = new Vector2(direction.x * chaseSpeed, rb.velocity.y);

        // 플레이어를 추적하는 중일 때
        if (distanceToPlayer > attackRange)
        {
            ChasePlayer();
        }
        else if (distanceToPlayer <= attackRange && Time.time >= nextAttackTime)
        {
            StartCoroutine(PerformJumpAttack());
        }
    }

    // 플레이어를 추적하는 함수
    private void ChasePlayer()
    {
        // 플레이어의 X좌표만 추적
        Vector2 direction = new Vector2(player.position.x - transform.position.x, 0).normalized;
        rb.velocity = new Vector2(direction.x * chaseSpeed, rb.velocity.y); // 추적 속도로 이동

        animator.SetBool("RoyalRun", true); // RoyalRun 애니메이션 실행
        animator.SetBool("RoyalIdle", false); // RoyalIdle 비활성화
    }

    // 점프 공격 패턴
    private IEnumerator PerformJumpAttack()
    {
        animator.SetBool("RoyalRun", false); // RoyalRun 멈춤
        animator.SetBool("RoyalIdle", true); // RoyalIdle 대기 애니메이션 실행

        // 공격 동작을 X축으로만 하도록 수정
        Vector2 attackDirection = new Vector2(player.position.x - transform.position.x, 0).normalized;
        rb.velocity = new Vector2(attackDirection.x, 0) * fallSpeed; // X축으로만 공격

        yield return new WaitForSeconds(0.3f); // 공격 후 대기

        rb.velocity = Vector2.zero; // 속도 0으로 설정하여 공격 후 멈춤
        nextAttackTime = Time.time + attackCooldown; // 공격 쿨타임 설정
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
        }
    }

    // 적이 죽었을 때 호출되는 함수
    public override void DieEffect()
    {
        base.DieEffect(); // TestEnemy의 DieEffect() 호출

        // 파티클 시스템 실행
        if (deathParticles != null)
        {
            Instantiate(deathParticles, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    // 적이 피해를 받는 함수
    public override void TakeHit(float damage, Vector2 hitPos)
    {
        base.TakeHit(damage, hitPos);

        // 적이 죽었을 때 처리
        if (health <= 0)
        {
            DieEffect(); // 사망 이펙트 호출
        }
    }
}
