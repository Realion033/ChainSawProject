using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class FinalAttack : Skill
{
    [SerializeField] private LayerMask WhatisEnemy;

    public float FinalAttackRaudius = 4;
    public float FinalAttackDamage = 100;
    private Animator _ani;
    public override void Init()
    {
        base.Init();
        _playerInput.isSkillUse += HandleSkillUse;

        _ani = GetComponent<Animator>();
    }
    public override void HandleSkillUse()
    {
        if (_skillEnum == Skills.FinalAttack)
        {
            if (PlayerCooldownManager.Instance.UseUlt())
            {
                UltAttack();
                OnAnimation();
            }
        }
    }

    private void OnAnimation()
    {
        transform.localScale = new Vector3(FinalAttackRaudius * 2, FinalAttackRaudius * 2, FinalAttackRaudius * 2);
        _ani.SetTrigger("FinalAttack");
        StartCoroutine(SetScaleDefault());
    }

    private IEnumerator SetScaleDefault()
    {
        yield return new WaitForSeconds(0.9f);
        transform.localScale = new Vector3(1, 1, 1);
    }

    private void UltAttack()
    {
        Collider2D[] enemys = Physics2D.OverlapCircleAll(transform.position, FinalAttackRaudius, WhatisEnemy);

        foreach (var enemy in enemys)
        {
            enemy.GetComponent<LivingEntity>().TakeHit(FinalAttackDamage, enemy.transform.position);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, FinalAttackRaudius);
    }
}
