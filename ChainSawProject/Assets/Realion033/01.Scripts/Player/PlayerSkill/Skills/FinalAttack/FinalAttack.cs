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
    }
    public override void HandleSkillUse()
    {
        base.HandleSkillUse();
        if (PlayerCooldownManager.Instance.UseUlt())
        {
            UltAttack();
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
