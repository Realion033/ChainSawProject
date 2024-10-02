using UnityEngine;

public class DieState : IEnemyState
{
    public void EnterState(EnemyFSM enemy)
    {
        Debug.Log("Enemy is Dead.");
        enemy.animator.SetTrigger("Die");  // Die 애니메이션 트리거
        enemy.GetComponent<Collider2D>().enabled = false;
    }

    public void UpdateState(EnemyFSM enemy)
    {
        // 죽은 상태에서 추가적인 업데이트가 필요하지 않음
    }

    public void ExitState(EnemyFSM enemy)
    {
        // Die 상태를 벗어나지 않음
    }
}
