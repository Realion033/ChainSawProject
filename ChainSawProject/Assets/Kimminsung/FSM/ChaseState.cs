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
        // 플레이어를 향하는 방향 벡터 계산
        Vector2 direction = (playerTransform.position - gameObject.transform.position).normalized;

        // 캐릭터의 위치를 플레이어 쪽으로 이동
        gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, playerTransform.position, moveSpeed * Time.deltaTime);

        // 방향에 따라 캐릭터의 스케일을 설정하여 방향을 반전
        if (direction.x > 0)
        {
            // 플레이어가 오른쪽에 있을 경우
            gameObject.transform.localScale = new Vector3(1f, 1f, 1f); // 원래 방향
        }
        else if (direction.x < 0)
        {
            // 플레이어가 왼쪽에 있을 경우
            gameObject.transform.localScale = new Vector3(-1f, 1f, 1f); // x축을 -1로 설정하여 반전
        }

        // 공격 가능 거리 내에 있으면 공격 상태로 전환
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
