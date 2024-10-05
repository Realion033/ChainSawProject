using UnityEngine;

public interface IEnemyState
{
    void EnterState(EnemyFSM enemy);
    void UpdateState(EnemyFSM enemy);
    void ExitState(EnemyFSM enemy);
}

public class EnemyFSM : MonoBehaviour
{
    public IEnemyState currentState;
    public Animator animator;  // Animator 추가
    public int damage = 5;
    public float health = 100f;
    public float maxHealth = 100f; // 최대 체력

    public IdleState idleState = new IdleState();
    public RunState runState = new RunState();
    public AttackState attackState = new AttackState();
    public DieState dieState = new DieState();

    public Transform player; // 플레이어의 위치
    public float detectionRange = 5f; // 플레이어를 감지할 범위
    public float attackRange = 1f; // 공격할 범위

    // 파티클 시스템 참조
    public ParticleSystem deathParticles;

    private void Start()
    {
        animator = GetComponent<Animator>();  // Animator 컴포넌트 가져오기
        currentState = idleState; // 초기 상태는 Idle
        currentState.EnterState(this);
        player = GameObject.FindGameObjectWithTag("KPlayer").transform;
    }

    private void Update()
    {
        if (health <= 0)
        {
            ChangeState(dieState);  // 체력이 0 이하일 때 죽는 상태로 전환
        }
        else
        {
            currentState.UpdateState(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("KPlayer") && attackState.isAttackState == true)
        {
            Player player = other.GetComponent<Player>(); // Player 스크립트 참조

            if (player != null)
            {
                player.TakeHit(damage, transform.position); // 플레이어에게 피해 전달
            }
        }
    }

    // 체력을 깎는 함수
    public void TakeHit(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die(); // 체력이 0이 되면 죽음
        }
    }

    // 적이 죽는 함수
    public void Die()
    {
        // 적의 현재 위치에서 파티클 시스템 재생
        if (deathParticles != null)
        {
            // 적의 현재 위치에서 파티클 재생
            Instantiate(deathParticles, transform.position, Quaternion.identity);
        }

        ChangeState(dieState); // 사망 상태로 전환
    }

    // 사망 이펙트 함수 - 파티클 재생 후 사망 처리
    public void DieEffect()
    {
        // 적의 사망 파티클을 재생하고 나서 적을 삭제
        if (deathParticles != null)
        {
            ParticleSystem particleInstance = Instantiate(deathParticles, transform.position, Quaternion.identity);
            particleInstance.Play(); // 파티클 시스템 실행
        }

        Destroy(gameObject); // 적을 삭제
    }

    public void ChangeState(IEnemyState newState)
    {
        currentState.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);
    }

    // 트리거 초기화 함수 추가
    public void ResetAllTriggers()
    {
        animator.ResetTrigger("Idle");
        animator.ResetTrigger("Run");
        animator.ResetTrigger("Attack");
        animator.ResetTrigger("Die");
    }
}
