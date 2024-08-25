using UnityEngine;

public class IdleState : State
{
    private Transform playerTransform;
    private float chaseDistance = 10f;

    public IdleState(GameObject gameObject, StateMachine stateMachine, Transform playerTransform) : base(gameObject, stateMachine)
    {
        this.playerTransform = playerTransform;
    }

    public override void Enter()
    {
        //Debug.Log("Entering Idle State");
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
        //Debug.Log("Exiting Idle State");
    }
}

public class ChaseState : State
{
    private Transform playerTransform;
    private float moveSpeed = 3f;
    private float attackDistance = 1.5f;
    private float attackDelay = 1.5f;
    private float attackTimer = 0f;

    public ChaseState(GameObject gameObject, StateMachine stateMachine, Transform playerTransform) : base(gameObject, stateMachine)
    {
        this.playerTransform = playerTransform;
    }

    public override void Enter()
    {
        //Debug.Log("Entering Chase State");
        attackTimer = 0f;
    }

    public override void Update()
    {
        Vector2 direction = (playerTransform.position - gameObject.transform.position).normalized;
        gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, playerTransform.position, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(gameObject.transform.position, playerTransform.position) <= attackDistance)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackDelay)
            {
                stateMachine.ChangeState(new AttackState(gameObject, stateMachine, playerTransform));
            }
        }
        else
        {
            attackTimer = 0f;
        }

        if (Vector2.Distance(gameObject.transform.position, playerTransform.position) >= 3f)
        {
            stateMachine.ChangeState(new IdleState(gameObject, stateMachine, playerTransform));
        }
    }

    public override void Exit()
    {
        Debug.Log("Exiting Chase State");
    }
}