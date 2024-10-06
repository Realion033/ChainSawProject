using System.Collections;
using TMPro;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class AttackState : IEnemyState
{
    private float attackCooldown = 2f;  // ���� ��Ÿ��
    public bool isAttackState;

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
        enemy.StopCoroutine(AttackRoutine(enemy));  // �ڷ�ƾ ����
        isAttackState=false;
    }

    private IEnumerator AttackRoutine(EnemyFSM enemy)
    {
        while (isAttackState)
        {
            float distanceToPlayer = Vector2.Distance(enemy.transform.position, enemy.player.position);

            // �÷��̾ ���� ���� ���� ���� ���� ����
            if (distanceToPlayer <= enemy.attackRange)
            {
                enemy.animator.SetTrigger("Attack");  // ���� �ִϸ��̼� Ʈ����
                Debug.Log("Enemy attacks!");
            }

            // ���� �� ��� �ð�
            yield return new WaitForSeconds(attackCooldown);  // ���� ��Ÿ�� ���
        }
    }
}
