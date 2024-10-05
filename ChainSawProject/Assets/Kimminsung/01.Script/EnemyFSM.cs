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

    public IdleState idleState = new IdleState();
    public RunState runState = new RunState();
    public AttackState attackState = new AttackState();
    public DieState dieState = new DieState();

    public Transform player; // 플레이어의 위치
    public float detectionRange = 5f; // 플레이어를 감지할 범위
    public float attackRange = 1f; // 공격할 범위
    public float health = 100f;

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
            ChangeState(dieState);
        }
        else
        {
            currentState.UpdateState(this);
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
