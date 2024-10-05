using UnityEngine;


public class IdleState : IEnemyState
{
    public void EnterState(EnemyFSM enemy)
    {
        enemy.animator.SetTrigger("Idle");  // Idle 애니메이션 트리거
    }

    public void UpdateState(EnemyFSM enemy)
    {
        float distanceToPlayer = Vector2.Distance(enemy.transform.position, enemy.player.position);
        if (distanceToPlayer <= enemy.detectionRange)
        {
            enemy.ChangeState(enemy.runState);
        }
    }

    public void ExitState(EnemyFSM enemy)
    {
        Debug.Log("Exiting Idle state.");
    }
}
