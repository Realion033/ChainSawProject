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
    public int damage = 5;
    public float health = 100f;
    public float maxHealth = 100f; // �ִ� ü��

    public IdleState idleState = new IdleState();
    public RunState runState = new RunState();
    public AttackState attackState = new AttackState();
    public DieState dieState = new DieState();

    public Transform player; // �÷��̾��� ��ġ
    public float detectionRange = 5f; // �÷��̾ ������ ����
    public float attackRange = 1f; // ������ ����

    // ��ƼŬ �ý��� ����
    public ParticleSystem deathParticles;

    private void Start()
    {
        animator = GetComponent<Animator>();  // Animator ������Ʈ ��������
        currentState = idleState; // �ʱ� ���´� Idle
        currentState.EnterState(this);
        player = GameObject.FindGameObjectWithTag("KPlayer").transform;
    }

    private void Update()
    {
        if (health <= 0)
        {
            ChangeState(dieState);  // ü���� 0 ������ �� �״� ���·� ��ȯ
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
            Player player = other.GetComponent<Player>(); // Player ��ũ��Ʈ ����

            if (player != null)
            {
                player.TakeHit(damage, transform.position); // �÷��̾�� ���� ����
            }
        }
    }

    // ü���� ��� �Լ�
    public void TakeHit(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die(); // ü���� 0�� �Ǹ� ����
        }
    }

    // ���� �״� �Լ�
    public void Die()
    {
        // ���� ���� ��ġ���� ��ƼŬ �ý��� ���
        if (deathParticles != null)
        {
            // ���� ���� ��ġ���� ��ƼŬ ���
            Instantiate(deathParticles, transform.position, Quaternion.identity);
        }

        ChangeState(dieState); // ��� ���·� ��ȯ
    }

    // ��� ����Ʈ �Լ� - ��ƼŬ ��� �� ��� ó��
    public void DieEffect()
    {
        // ���� ��� ��ƼŬ�� ����ϰ� ���� ���� ����
        if (deathParticles != null)
        {
            ParticleSystem particleInstance = Instantiate(deathParticles, transform.position, Quaternion.identity);
            particleInstance.Play(); // ��ƼŬ �ý��� ����
        }

        Destroy(gameObject); // ���� ����
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
