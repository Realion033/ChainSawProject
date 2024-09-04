using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public LayerMask WhatisEnemy;
    //public bool isHit = false;
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
        Collider2D[] enemys = Physics2D.OverlapCircleAll(transform.position, 0.6f, WhatisEnemy);

        if (_playerInput.isSlash)
        {
            for (int i = 0; i < enemys.Length; i++)
            {
                //isHit = true;
                if (PlayerCooldownManager.Instance.AttackTick())
                {
                    enemys[i].GetComponent<LivingEntity>().TakeHit(30, Vector2.zero);
                }
            }
            //isHit = false;
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.6f);
    }
}
