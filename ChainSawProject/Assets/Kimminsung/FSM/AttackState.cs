
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AttackState : EnemyState
{

    private float attackCooldown = 1f;
    private float attackTimer = 0f;
    private int damage = 12;
    private float attackDistance = 1.5f;

    public AttackState(Enemy_Knife enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {
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

        if (Vector2.Distance(_owner.transform.position, GameManager.instance.PlayerTrm.position) > attackDistance)
        {
            stateMachine.ChangeState(new ChaseState(_owner, stateMachine));
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
        M_PlayerHealth playerHealth = GameManager.instance.PlayerTrm.GetComponent<M_PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);    
        }
    }
}