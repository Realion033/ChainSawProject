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
    public Animator animator;  // Animator �߰�

    public IdleState idleState = new IdleState();
    public RunState runState = new RunState();
    public AttackState attackState = new AttackState();
    public DieState dieState = new DieState();

    public Transform player; // �÷��̾��� ��ġ
    public float detectionRange = 5f; // �÷��̾ ������ ����
    public float attackRange = 1f; // ������ ����
    public float health = 100f;

    private void Start()
    {
        animator = GetComponent<Animator>();  // Animator ������Ʈ ��������
        currentState = idleState; // �ʱ� ���´� Idle
        currentState.EnterState(this);
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

    public void ChangeState(IEnemyState newState)
    {
        currentState.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);
    }

    // Ʈ���� �ʱ�ȭ �Լ� �߰�
    public void ResetAllTriggers()
    {
        animator.ResetTrigger("Idle");
        animator.ResetTrigger("Run");
        animator.ResetTrigger("Attack");
        animator.ResetTrigger("Die");
    }
}