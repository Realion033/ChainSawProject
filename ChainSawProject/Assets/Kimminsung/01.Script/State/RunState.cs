using UnityEngine;

public class RunState : IEnemyState
{
    public void EnterState(EnemyFSM enemy)
    {
        enemy.ResetAllTriggers();  // Ʈ���� �ʱ�ȭ
        enemy.animator.SetTrigger("Run");  // �޸��� �ִϸ��̼� Ʈ����
        Debug.Log("Enemy is now Running.");
    }

    public void UpdateState(EnemyFSM enemy)
    {
        float distanceToPlayer = Vector2.Distance(enemy.transform.position, enemy.player.position);

        // �÷��̾ ���� ���� �ȿ� ������ AttackState�� ��ȯ
        if (distanceToPlayer <= enemy.attackRange)
        {
            enemy.ChangeState(enemy.attackState);
        }
        else
        {
            // �÷��̾ ��� ���󰡵��� �̵�
            Vector2 direction = (enemy.player.position - enemy.transform.position).normalized;
            enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, enemy.player.position, Time.deltaTime * 2);  // �̵� �ӵ� ���� ����
        }
    }

    public void ExitState(EnemyFSM enemy)
    {
        Debug.Log("Exiting Run state.");
    }
}
