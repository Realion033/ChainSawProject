    using UnityEngine;

public class IdleState : State
{
    private Transform playerTransform;
    private float chaseDistance = 10f;

    public IdleState(GameObject gameObject, StateMachine stateMachine, Transform playerTransform) :     base(gameObject, stateMachine)
    {
        this.playerTransform = playerTransform;
    }

    public override void Enter()
    {
        Debug.Log("Entering Idle State");
    }

    public override void Update()
    {
        // �÷��̾ ���� �Ÿ� �̳��� �ִ��� Ȯ��
        if (Vector2.Distance(gameObject.transform.position, playerTransform.position) < chaseDistance)
        {
            stateMachine.ChangeState(new ChaseState(gameObject, stateMachine, playerTransform));
        }
    }

    public override void Exit()
    {
        Debug.Log("Exiting Idle State");
    }
}

