using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private LayerMask WhatisEnemy;
    [SerializeField] private PlayerStatSO _playerStat;
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

        if (_playerInput.isSlash && enemys != null)
        {
            foreach (var enemy in enemys)
            {
                if (PlayerCooldownManager.Instance.AttackTick())
                {
                    enemy.GetComponent<LivingEntity>().TakeHit(_playerStat.playerDamage, Vector2.zero);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.6f);
    }
}
