using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : EnemyState
{

    private float moveSpeed = 3f;
    private float attackDistance = 1.5f;
    private float attackDelay = 1.5f;
    private float attackTimer = 0f;

    public ChaseState(Enemy_Knife enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log("Entering Chase State");
        attackTimer = 0f;
    }

    public override void Update()
    {
        Chase();
        if (Vector2.Distance(_owner.transform.position, GameManager.instance.PlayerTrm.position) >= 3f)
        {
            stateMachine.ChangeState(new IdleState(_owner, stateMachine));
        }
    }

    public void Chase()
    {
        // �÷��̾ ���ϴ� ���� ���� ���
        Vector2 direction = (GameManager.instance.PlayerTrm.position - _owner.transform.position).normalized;

        // ĳ������ ��ġ�� �÷��̾� ������ �̵�
        _owner.transform.position = Vector2.MoveTowards(_owner.transform.position, GameManager.instance.PlayerTrm.position, moveSpeed * Time.deltaTime);

        // ���⿡ ���� ĳ������ �������� �����Ͽ� ������ ����
        if (direction.x > 0)
        {
            // �÷��̾ �����ʿ� ���� ���
            _owner.transform.localScale = new Vector3(1f, 1f, 1f); // ���� ����
            _owner.anim.KnifeEnemRun();
            
        }
        else if (direction.x < 0)
        {
            // �÷��̾ ���ʿ� ���� ���
            _owner.transform.localScale = new Vector3(-1f, 1f, 1f); // x���� -1�� �����Ͽ� ����
            _owner.anim.KnifeEnemRun();
        }

        // ���� ���� �Ÿ� ���� ������ ���� ���·� ��ȯ
        if (Vector2.Distance(_owner.transform.position, GameManager.instance.PlayerTrm.position) <= attackDistance)
        {
            _owner.anim.KnifeEnemRunf();
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackDelay)
            {
                stateMachine.ChangeState(new AttackState(_owner, stateMachine));
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
