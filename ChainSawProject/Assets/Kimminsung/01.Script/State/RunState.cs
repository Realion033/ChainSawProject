using UnityEngine;

public class RunState : IEnemyState
{
    public void EnterState(EnemyFSM enemy)
    {
        enemy.ResetAllTriggers();  // 트리거 초기화
        enemy.animator.SetTrigger("Run");  // 달리기 애니메이션 트리거
        Debug.Log("Enemy is now Running.");
    }

    public void UpdateState(EnemyFSM enemy)
    {
        float distanceToPlayer = Vector2.Distance(enemy.transform.position, enemy.player.position);

        // 플레이어가 공격 범위 안에 들어오면 AttackState로 전환
        if (distanceToPlayer <= enemy.attackRange)
        {
            enemy.ChangeState(enemy.attackState);
        }
    }

    public void ExitState(EnemyFSM enemy)
    {
        Debug.Log("Exiting Run state.");
    }
}
