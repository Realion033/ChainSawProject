using System.Collections;
using TMPro;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class AttackState : IEnemyState
{
    private float attackCooldown = 2f;  // 공격 쿨타임
    public bool isAttackState;

    public void EnterState(EnemyFSM enemy)
    {
        enemy.ResetAllTriggers();  // 모든 트리거 초기화
        enemy.animator.SetTrigger("Attack");  // Attack 트리거 설정
        Debug.Log("Enemy is now Attacking.");
        enemy.StartCoroutine(AttackRoutine(enemy));  // 코루틴 시작
        isAttackState = true;
    }

    public void UpdateState(EnemyFSM enemy)
    {
        float distanceToPlayer = Vector2.Distance(enemy.transform.position, enemy.player.position);

        // 플레이어가 공격 범위를 벗어나면 RunState로 전환
        if (distanceToPlayer > enemy.attackRange)
        {
            enemy.ChangeState(enemy.runState);  // RunState로 전환
        }
    }

    public void ExitState(EnemyFSM enemy)
    {
        Debug.Log("Exiting Attack state.");
        enemy.StopCoroutine(AttackRoutine(enemy));  // 코루틴 정지
        isAttackState=false;
    }

    private IEnumerator AttackRoutine(EnemyFSM enemy)
    {
        while (isAttackState)
        {
            float distanceToPlayer = Vector2.Distance(enemy.transform.position, enemy.player.position);

            // 플레이어가 공격 범위 내에 있을 때만 공격
            if (distanceToPlayer <= enemy.attackRange)
            {
                enemy.animator.SetTrigger("Attack");  // 공격 애니메이션 트리거
                Debug.Log("Enemy attacks!");
            }

            // 공격 후 대기 시간
            yield return new WaitForSeconds(attackCooldown);  // 공격 쿨타임 대기
        }
    }
}
