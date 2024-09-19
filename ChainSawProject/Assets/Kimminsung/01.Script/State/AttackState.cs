using UnityEngine;

public class AttackState : IEnemyState
{
    private float attackCooldown = 2f;
    private float lastAttackTime = 0f;

    public void EnterState(EnemyFSM enemy)
    {
        enemy.ResetAllTriggers();  // 모든 트리거 초기화
        enemy.animator.SetTrigger("Attack");  // Attack 트리거 설정
        Debug.Log("Enemy is now Attacking.");
        Attack(enemy);  // 처음 공격
    }

    public void UpdateState(EnemyFSM enemy)
    {
        float distanceToPlayer = Vector2.Distance(enemy.transform.position, enemy.player.position);

        if (Time.time - lastAttackTime >= attackCooldown && distanceToPlayer <= enemy.attackRange)
        {
            enemy.animator.SetTrigger("Attack");  // 공격 애니메이션 트리거
            Attack(enemy);  // 플레이어 공격
            enemy.animator.SetTrigger("Idle");
        }

       

        // 플레이어가 공격 범위를 벗어나면 RunState로 전환
        if (distanceToPlayer > enemy.attackRange)
        {
            enemy.ChangeState(enemy.runState);  // RunState로 전환
        }
    }

    public void ExitState(EnemyFSM enemy)
    {
        Debug.Log("Exiting Attack state.");
    }

    private void Attack(EnemyFSM enemy)
    {
        Debug.Log("Enemy attacks!");
        lastAttackTime = Time.time;  // 공격 타임스탬프 갱신
    }
}
