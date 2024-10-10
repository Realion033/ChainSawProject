using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private LayerMask WhatisEnemy;
    [SerializeField] private PlayerStatSO _playerStat;
    [HideInInspector] public float damageCasterRadius = 0.6f;
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
        Collider2D[] enemys = Physics2D.OverlapCircleAll(transform.position, damageCasterRadius, WhatisEnemy);

        if (PlayerCooldownManager.Instance.AttackTick())
        {
            if (_playerInput.isSlash && enemys != null)
            {
                foreach (var enemy in enemys)
                {
                    // float rand = UnityEngine.Random.Range(-0.2f, 0.2f);
                    // Transform enemytr = new Vector2(enemy.transform.position.x, enemy.transform.position.rand, 0);
                    enemy.GetComponent<LivingEntity>().TakeHit(_playerStat.playerDamage, enemy.transform.position);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, damageCasterRadius);
    }
}