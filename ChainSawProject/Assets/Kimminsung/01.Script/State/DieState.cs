using UnityEngine;

public class DieState : IEnemyState
{
    public void EnterState(EnemyFSM enemy)
    {
        Debug.Log("Enemy is Dead.");
        enemy.animator.SetTrigger("Die");  // Die �ִϸ��̼� Ʈ����
        enemy.GetComponent<Collider2D>().enabled = false;
    }

    public void UpdateState(EnemyFSM enemy)
    {
        // ���� ���¿��� �߰����� ������Ʈ�� �ʿ����� ����
    }

    public void ExitState(EnemyFSM enemy)
    {
        // Die ���¸� ����� ����
    }
}
