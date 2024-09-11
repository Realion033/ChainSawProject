using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalAttack : Skill
{
    [SerializeField] private LayerMask WhatisEnemy;

    public float FinalAttackRaudius = 4;
    public float FinalAttackDamage = 100;
    public override void Init()
    {
        base.Init();
        _playerInput.isSkillUse += HandleSkillUse;
    }
    public override void HandleSkillUse()
    {
        if (_skillEnum == Skills.FinalAttack)
        {
            if (PlayerCooldownManager.Instance.UseUlt())
            {
                UltAttack();
                Debug.Log("ULT on!");
            }
        }
    }

    private void UltAttack()
    {
        Collider2D[] enemys = Physics2D.OverlapCircleAll(transform.position, FinalAttackRaudius, WhatisEnemy);

        foreach (var enemy in enemys)
        {
            enemy.GetComponent<LivingEntity>().TakeHit(FinalAttackDamage, Vector2.zero);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, FinalAttackRaudius);
    }
}
