
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
        //Debug.Log("Entering Attack State");
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
        //Debug.Log("Exiting Attack State");
    }

    private void PerformAttack()
    {
<<<<<<< HEAD:ChainSawProject/Assets/Kimminsung/AttackState.cs
        // ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½
        //Debug.Log("Performing Attack");
        // ï¿½ï¿½ï¿½â¿¡ ï¿½Ã·ï¿½ï¿½Ì¾î¿¡ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½Ö´ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ß°ï¿½
        M_PlayerHealth playerHealth = playerTransform.GetComponent<M_PlayerHealth>();
=======
        // °ø°Ý ·ÎÁ÷ ±¸Çö
        Debug.Log("Performing Attack");
        // ¿©±â¿¡ ÇÃ·¹ÀÌ¾î¿¡°Ô µ¥¹ÌÁö¸¦ ÁÖ´Â ·ÎÁ÷À» Ãß°¡
        M_PlayerHealth playerHealth = GameManager.instance.PlayerTrm.GetComponent<M_PlayerHealth>();
>>>>>>> Kiminsung:ChainSawProject/Assets/Kimminsung/FSM/AttackState.cs
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);    
        }
    }
}