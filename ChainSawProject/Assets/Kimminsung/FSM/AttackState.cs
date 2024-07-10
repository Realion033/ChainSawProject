
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AttackState : State
{
    private Transform playerTransform;
    private float attackCooldown = 1f;
    private float attackTimer = 0f;
    private int damage = 12;
    private float attackDistance = 1.5f;

    public AttackState(GameObject gameObject, StateMachine stateMachine, Transform playerTransform) : base(gameObject, stateMachine)
    {
        this.playerTransform = playerTransform;
    }

    public override void Enter()
    {
        Debug.Log("Entering Attack State");
        attackTimer = 0f;
    }

    public override void Update()
    {
        attackTimer += Time.deltaTime;

        if (attackTimer >= attackCooldown)
        {
            PerformAttack();
            attackTimer = 0f;
        }

        if (Vector2.Distance(gameObject.transform.position, playerTransform.position) > attackDistance)
        {
            stateMachine.ChangeState(new ChaseState(gameObject, stateMachine, playerTransform));
        }
    }

    public override void Exit()
    {
        Debug.Log("Exiting Attack State");
    }

    private void PerformAttack()
    {
        // 공격 로직 구현
        Debug.Log("Performing Attack");
        // 여기에 플레이어에게 데미지를 주는 로직을 추가
        M_PlayerHealth playerHealth = playerTransform.GetComponent<M_PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);    
        }
    }
}