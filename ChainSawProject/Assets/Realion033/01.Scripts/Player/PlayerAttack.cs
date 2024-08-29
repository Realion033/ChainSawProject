using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        Attack();
    }

    private void Attack()
    {
        Collider2D[] enemys = Physics2D.OverlapCircleAll(transform.position, 0.6f, 1 << 7);

        if (_playerInput.isSlash)
        {
            if (PlayerCooldownManager.Instance.AttackTick())
            {
                for (int i = 0; i < enemys.Length; i++)
                {
                    enemys[i].GetComponent<LivingEntity>().TakeHit(30, Vector2.zero);
                }
            }
        }
    }

    //사실상 공격처리
    // private void OnCollisionStay2D(Collision2D other)
    // {
    //     try
    //     {
    //         if (_playerInput.isSlash)
    //         {
    //             if (PlayerCooldownManager.Instance.AttackTick())
    //             {
    //                 other.gameObject.GetComponent<LivingEntity>().TakeHit(30, Vector2.zero);
    //             }
    //         }
    //     }
    //     catch (Exception e)
    //     {
    //         Debug.Log($"Collider Not have \"LivingEntitiy\" (No problem) >> {e.Message}");
    //     }
    // }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.6f);
    }
}
