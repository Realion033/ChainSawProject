using System;
using System.Collections;
using UnityEngine;

public class RangedEnemy : TestEnemy
{
    public float chaseSpeed = 2f; // 플레이어 추적 속도
    public float chaseRange = 10f; // 플레이어를 추적할 범위
    public float stopDistance = 2f; // 일정 거리까지 플레이어 추적 후 멈춤

    private Transform player; // 플레이어의 위치를 추적하기 위한 변수
    private Rigidbody2D rb; // Rigidbody2D 참조
    private Animator animator; // 애니메이터 참조

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
        // 플레이어의 위치에서 X 좌표만 사용하고, Y는 현재 적의 Y 위치를 고정
        Vector2 direction = new Vector2(player.position.x - transform.position.x, 0).normalized;

        // 추적 속도만큼 이동 (Y축은 0으로 고정됨)
        rb.velocity = new Vector2(direction.x * chaseSpeed, rb.velocity.y);
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
        StartCoroutine(Hitstop());
        player.GetComponent<LivingEntity>().health = player.GetComponent<Player>()._playerStat.playerHealth;
    }

    private IEnumerator Hitstop()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(0.16f);
        Time.timeScale = 1;
    }

    // 2초 후 오브젝트 제거 코루틴
    private IEnumerator RemoveAfterDeath()
    {
        yield return new WaitForSeconds(0.1f);
        // 부모 오브젝트를 삭제하여 자식 오브젝트도 함께 제거
        Destroy(gameObject); // 2초 후 적 제거
    }

}
