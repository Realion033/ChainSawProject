using UnityEngine;

public class AttackState : IEnemyState
{
    private float attackCooldown = 2f;
    private float lastAttackTime = 0f;

    public void EnterState(EnemyFSM enemy)
    {
        enemy.ResetAllTriggers();  // ��� Ʈ���� �ʱ�ȭ
        enemy.animator.SetTrigger("Attack");  // Attack Ʈ���� ����
        Debug.Log("Enemy is now Attacking.");
        Attack(enemy);  // ó�� ����
    }

    public void UpdateState(EnemyFSM enemy)
    {
        float distanceToPlayer = Vector2.Distance(enemy.transform.position, enemy.player.position);

        if (Time.time - lastAttackTime >= attackCooldown && distanceToPlayer <= enemy.attackRange)
        {
            enemy.animator.SetTrigger("Attack");  // ���� �ִϸ��̼� Ʈ����
            Attack(enemy);  // �÷��̾� ����
            enemy.animator.SetTrigger("Idle");
        }

       

        // �÷��̾ ���� ������ ����� RunState�� ��ȯ
        if (distanceToPlayer > enemy.attackRange)
        {
            enemy.ChangeState(enemy.runState);  // RunState�� ��ȯ
        }
    }

    public void ExitState(EnemyFSM enemy)
    {
        Debug.Log("Exiting Attack state.");
    }

    private void Attack(EnemyFSM enemy)
    {
        Debug.Log("Enemy attacks!");
        lastAttackTime = Time.time;  // ���� Ÿ�ӽ����� ����
    }
}
