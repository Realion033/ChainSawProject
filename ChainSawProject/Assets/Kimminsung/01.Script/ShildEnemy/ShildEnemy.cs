using System.Collections;
using UnityEngine;

public class ShildEnemy : TestEnemy
{
    public float jumpHeight = 5f; // 점프 높이
    public float fallSpeed = 10f; // 낙하 속도
    public float attackCooldown = 7f; // 공격 쿨타임
    public float attackRange = 10f; // 공격 범위
    public float chaseSpeed = 3f; // 추적 속도
    public ParticleSystem deathParticles; // 죽을 때 사용할 파티클 시스템
    public float damage = 10f;

    private Animator animator;
    private Transform player; // 플레이어의 위치를 추적하기 위한 변수
    private Rigidbody2D rb; // Rigidbody2D 참조
    private float nextAttackTime = 0f; // 다음 공격까지 남은 시간
    private bool isAttacking = false; // 공격 중인지 여부를 나타내는 변수

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D 컴포넌트
        player = GameObject.FindGameObjectWithTag("KPlayer").transform; // 플레이어 게임 오브젝트 찾기
        health = 100f; // RoyalEnemy의 체력
        maxHealth = health; // 최대 체력 설정
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isDead) return;

        if (health <= 0)
        {
            Die();
            return;
        }

        float distanceToPlayer = Mathf.Abs(player.position.x - transform.position.x); // 플레이어와의 거리 계산

        // 쿨다운 체크 추가
        if (Time.time < nextAttackTime)
        {
            // 쿨다운 동안 플레이어를 추적만 하도록 설정
            Vector2 direction = new Vector2(player.position.x - transform.position.x, 0).normalized;
            rb.velocity = new Vector2(direction.x * chaseSpeed, rb.velocity.y);
            return;
        }

        Vector2 chaseDirection = new Vector2(player.position.x - transform.position.x, 0).normalized;

        if (distanceToPlayer > attackRange)
        {
            ChasePlayer();
        }
        else if (distanceToPlayer <= attackRange)
        {
            StartCoroutine(PerformJumpAttack());
        }
    }

    private void ChasePlayer()
    {
        Vector2 direction = new Vector2(player.position.x - transform.position.x, 0).normalized;
        if (direction.x > 0.3f)
        {
            rb.velocity = new Vector2(direction.x * chaseSpeed, rb.velocity.y);

            animator.SetBool("SheildRun", true);
            animator.SetBool("SheildIdle", false);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private IEnumerator PerformJumpAttack()
    {
        if (isAttacking) yield break; // 이미 공격 중이면 종료

        isAttacking = true;
        animator.SetBool("SheildRun", true);
        animator.SetBool("SheildIdle", false);

        // 공격 방향 계산
        Vector2 attackDirection = new Vector2(player.position.x - transform.position.x, 0).normalized;

        // X축으로만 이동하기 위한 속도 설정
        float xMovement = 10f;
        rb.velocity = new Vector2(xMovement * Mathf.Sign(attackDirection.x), 0);

        // X축으로 이동 유지
        yield return new WaitForSeconds(0.5f);

        // 공격 종료 후 상태 설정
        rb.velocity = Vector2.zero;
        nextAttackTime = Time.time + attackCooldown; // 쿨다운 시간 설정

        isAttacking = false;
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.collider.CompareTag("KPlayer") && isAttacking)
        {
            Player player = other.collider.GetComponent<Player>();

            if (player != null)
            {
                player.TakeHit(damage, transform.position);
            }
        }
    }

    public override void DieEffect()
    {
        base.DieEffect();

        if (deathParticles != null)
        {
            Instantiate(deathParticles, transform.position, Quaternion.identity);
        }

        StartCoroutine(DieAndPause());
        player.GetComponent<LivingEntity>().health = player.GetComponent<Player>()._playerStat.playerHealth;
    }

    private IEnumerator DieAndPause()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(0.16f);
        Time.timeScale = 1;
        Destroy(gameObject);
    }

    public override void TakeHit(float damage, Vector2 hitPos)
    {
        base.TakeHit(damage, hitPos);

        if (health <= 0)
        {
            DieEffect();
        }
    }
}
