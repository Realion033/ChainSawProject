using System.Collections;
using UnityEngine;

public class RangedEnemy : TestEnemy
{
    public float chaseSpeed = 2f; // 플레이어 추적 속도
    public float chaseRange = 10f; // 플레이어를 추적할 범위
    public float stopDistance = 2f; // 일정 거리까지 플레이어 추적 후 멈춤
    public float attackRange = 1.5f; // 공격 거리
    public float attackDamage = 10f; // 공격 데미지
    public float attackCooldown = 1f; // 공격 쿨다운 시간

    private Transform player; // 플레이어의 위치를 추적하기 위한 변수
    private Rigidbody2D rb; // Rigidbody2D 참조
    private Animator animator; // 애니메이터 참조
    private float lastAttackTime; // 마지막 공격 시간

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D 컴포넌트 가져오기
        player = GameObject.FindGameObjectWithTag("KPlayer").transform; // 플레이어 오브젝트 찾기
        animator = GetComponent<Animator>(); // Animator 컴포넌트 가져오기
        health = 25f; // RangedEnemy의 초기 체력
        maxHealth = health; // 체력 슬라이더를 위한 최대 체력 설정
    }

    private void Update()
    {
        // 적이 죽었으면 더 이상 로직을 실행하지 않음
        if (isDead) return;

        if (player != null && health > 0)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            // 플레이어가 추적 범위 내에 있을 때만 이동
            if (distanceToPlayer < chaseRange && distanceToPlayer > stopDistance)
            {
                ChasePlayer();
                animator.SetBool("BulletRun", true); // 이동 중인 애니메이션 재생
                animator.SetBool("BulletIdle", false); // 대기 상태는 끔

                // 공격 거리 내에 있을 경우 공격 시도
                if (distanceToPlayer <= attackRange && Time.time >= lastAttackTime + attackCooldown)
                {
                    AttackPlayer();
                }
            }
            else
            {
                rb.velocity = Vector2.zero; // 멈춤
                animator.SetBool("BulletRun", false); // 이동 중인 애니메이션 끔
                animator.SetBool("BulletIdle", true); // 대기 상태로 전환
            }
        }

        // 적이 죽으면 사망 처리
        if (health <= 0 && !isDead)
        {
            DieEffect(); // TestEnemy의 사망 이펙트 함수 호출
        }
    }

    // 플레이어를 추적하는 함수
    private void ChasePlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized; // 플레이어 쪽으로 방향 계산
        rb.velocity = direction * chaseSpeed; // 추적 속도만큼 이동
    }

    // 플레이어를 공격하는 함수
    private void AttackPlayer()
    {
        lastAttackTime = Time.time; // 마지막 공격 시간 업데이트
        Player playerScript = player.GetComponent<Player>(); // Player 스크립트 참조

        if (playerScript != null)
        {
            playerScript.TakeHit(attackDamage, transform.position); // 플레이어에 공격 피해 전달
            animator.SetTrigger("Attack"); // 공격 애니메이션 트리거
        }
    }

    // 피해 받는 함수 (TestEnemy의 TakeHit() 활용)
    public override void TakeHit(float damage, Vector2 hitPos)
    {
        if (isDead) return; // 이미 죽은 적은 데미지 처리 안 함

        // 체력 감소 처리
        base.TakeHit(damage, hitPos);

        // 적이 죽었을 때 처리
        if (health <= 0 && !isDead)
        {
            DieEffect(); // 사망 이펙트 호출
        }
    }

    // DieEffect 오버라이드 (TestEnemy의 DieEffect() 호출)
    public override void DieEffect()
    {
        base.DieEffect(); // TestEnemy의 DieEffect() 실행
        StartCoroutine(RemoveAfterDeath()); // 2초 후 오브젝트 제거
    }

    // 2초 후 오브젝트 제거 코루틴
    private IEnumerator RemoveAfterDeath()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject); // 2초 후 적 제거
    }
}
