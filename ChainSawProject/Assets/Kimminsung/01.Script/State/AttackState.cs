using System.Collections;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class AttackState : IEnemyState
{
    public float attackCooldown = 5f;  // ���� ��Ÿ��
    public bool isAttackState;
    private bool canAttack = true; // ���� ���� ���θ� ��Ÿ���� �÷���

    public void EnterState(EnemyFSM enemy)
    {
        enemy.ResetAllTriggers();  // ��� Ʈ���� �ʱ�ȭ
        enemy.animator.SetTrigger("Attack");  // Attack Ʈ���� ����
        Debug.Log("Enemy is now Attacking.");
        enemy.StartCoroutine(AttackRoutine(enemy));  // �ڷ�ƾ ����
        isAttackState = true;
    }

    public void UpdateState(EnemyFSM enemy)
    {
        float distanceToPlayer = Vector2.Distance(enemy.transform.position, enemy.player.position);

        // �÷��̾ ���� ������ ����� RunState�� ��ȯ
        if (distanceToPlayer > enemy.attackRange)
        {
            enemy.ChangeState(enemy.runState);  // RunState�� ��ȯ
        }
    }

    public void ExitState(EnemyFSM enemy)
    {
        Debug.Log("Exiting Attack state.");
        isAttackState = false;
        enemy.StopCoroutine(AttackRoutine(enemy));  // �ڷ�ƾ ����
    }

    private IEnumerator AttackRoutine(EnemyFSM enemy)
    {
        while (isAttackState)
        {
            float distanceToPlayer = Vector2.Distance(enemy.transform.position, enemy.player.position);

            // �÷��̾ ���� ���� ���� ���� ���� ����
            if (distanceToPlayer <= enemy.attackRange && canAttack)
            {
                enemy.animator.SetTrigger("Attack");  // ���� �ִϸ��̼� Ʈ����
                Debug.Log("Enemy attacks!");

                // ���� �� ��Ÿ�� ����
                canAttack = false;
                yield return new WaitForSeconds(attackCooldown);  // ���� ��Ÿ�� ���
                canAttack = true; // ��Ÿ���� ������ �ٽ� ���� ����
            }
            else
            {
                // �÷��̾ ���� ������ ��� ��� ���
                yield return null; // ���� ���������� �̵�
            }
        }
    }
}
