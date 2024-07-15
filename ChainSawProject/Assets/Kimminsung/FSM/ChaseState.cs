using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Debug.Log("Entering Chase State");
        attackTimer = 0f;
    }

    public override void Update()
    {
        Chase();
        if (Vector2.Distance(gameObject.transform.position, playerTransform.position) >= 3f)
        {
            stateMachine.ChangeState(new IdleState(gameObject, stateMachine, playerTransform));
        }
    }

    public void Chase()
    {
        // �÷��̾ ���ϴ� ���� ���� ���
        Vector2 direction = (playerTransform.position - gameObject.transform.position).normalized;

        // ĳ������ ��ġ�� �÷��̾� ������ �̵�
        gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, playerTransform.position, moveSpeed * Time.deltaTime);

        // ���⿡ ���� ĳ������ �������� �����Ͽ� ������ ����
        if (direction.x > 0)
        {
            // �÷��̾ �����ʿ� ���� ���
            gameObject.transform.localScale = new Vector3(1f, 1f, 1f); // ���� ����
        }
        else if (direction.x < 0)
        {
            // �÷��̾ ���ʿ� ���� ���
            gameObject.transform.localScale = new Vector3(-1f, 1f, 1f); // x���� -1�� �����Ͽ� ����
        }

        // ���� ���� �Ÿ� ���� ������ ���� ���·� ��ȯ
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
    }

    public override void Exit()
    {
        Debug.Log("Exiting Chas");
    } 
}
