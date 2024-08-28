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
        // 플레이어를 향하는 방향 벡터 계산
        Vector2 direction = (GameManager.instance.PlayerTrm.position - _owner.transform.position).normalized;

        // 캐릭터의 위치를 플레이어 쪽으로 이동
        _owner.transform.position = Vector2.MoveTowards(_owner.transform.position, GameManager.instance.PlayerTrm.position, moveSpeed * Time.deltaTime);

        // 방향에 따라 캐릭터의 스케일을 설정하여 방향을 반전
        if (direction.x > 0)
        {
            // 플레이어가 오른쪽에 있을 경우
            _owner.transform.localScale = new Vector3(1f, 1f, 1f); // 원래 방향
            _owner.anim.KnifeEnemRun();
            
        }
        else if (direction.x < 0)
        {
            // 플레이어가 왼쪽에 있을 경우
            _owner.transform.localScale = new Vector3(-1f, 1f, 1f); // x축을 -1로 설정하여 반전
            _owner.anim.KnifeEnemRun();
        }

        // 공격 가능 거리 내에 있으면 공격 상태로 전환
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
