using System.Collections;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class AttackState : IEnemyState
{
    public float attackCooldown = 5f;  // 공격 쿨타임
    public bool isAttackState;
    private bool canAttack = true; // 공격 가능 여부를 나타내는 플래그

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
        isAttackState = false;
        enemy.StopCoroutine(AttackRoutine(enemy));  // 코루틴 정지
    }

    private IEnumerator AttackRoutine(EnemyFSM enemy)
    {
        while (isAttackState)
        {
            float distanceToPlayer = Vector2.Distance(enemy.transform.position, enemy.player.position);

            // 플레이어가 공격 범위 내에 있을 때만 공격
            if (distanceToPlayer <= enemy.attackRange && canAttack)
            {
                enemy.animator.SetTrigger("Attack");  // 공격 애니메이션 트리거
                Debug.Log("Enemy attacks!");

                // 공격 후 쿨타임 설정
                canAttack = false;
                yield return new WaitForSeconds(attackCooldown);  // 공격 쿨타임 대기
                canAttack = true; // 쿨타임이 끝나면 다시 공격 가능
            }
            else
            {
                // 플레이어가 공격 범위를 벗어난 경우 대기
                yield return null; // 다음 프레임으로 이동
            }
        }
    }
}
