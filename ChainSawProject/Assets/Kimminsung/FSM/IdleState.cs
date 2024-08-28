    using UnityEngine;

public class IdleState : EnemyState
{
    private float chaseDistance = 10f;

    public IdleState(Enemy_Knife enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log("Entering Idle State");
    }

    public override void Update()
    {
        // 플레이어가 일정 거리 이내에 있는지 확인
        if (Vector2.Distance(_owner.transform.position, GameManager.instance.PlayerTrm.position) < chaseDistance)
        {
            stateMachine.ChangeState(new ChaseState(_owner, stateMachine));
        }
    }

    public override void Exit()
    {
        Debug.Log("Exiting Idle State");
    }
}

