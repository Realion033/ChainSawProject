using UnityEngine;

public class IdleState : IEnemyState
{
    public void EnterState(EnemyFSM enemy)
    {
        if (enemy.animator != null)
        {
            enemy.animator.SetTrigger("Idle");  // Idle �ִϸ��̼� Ʈ����
        }
        else
        {
            Debug.LogWarning("Animator is null, cannot set Idle trigger.");
        }
    }

    public void UpdateState(EnemyFSM enemy)
    {
        if (enemy.player == null)
        {
            Debug.LogWarning("Player reference is null, cannot calculate distance.");
            return;
        }

        // Vector2.Distance ��� sqrMagnitude ������� ����ȭ
        float distanceToPlayerSqr = (enemy.transform.position - enemy.player.position).sqrMagnitude;
        float detectionRangeSqr = enemy.detectionRange * enemy.detectionRange;

        if (distanceToPlayerSqr <= detectionRangeSqr)
        {
            enemy.ChangeState(enemy.runState);
        }
    }

    public void ExitState(EnemyFSM enemy)
    {
        Debug.Log("Exiting Idle state.");
    }
}
