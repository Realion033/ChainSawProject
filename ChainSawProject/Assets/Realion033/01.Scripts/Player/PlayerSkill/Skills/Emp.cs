using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emp : Skill
{
    [SerializeField] private LayerMask WhatisEnemy;

    public float EmpRaudius = 4;
    public float EmpTime = 4;
    public float EmpDamageInc = 1.2f;
    private Animator _ani;
    public override void Init()
    {
        base.Init();
        _playerInput.isSkillUse += HandleSkillUse;

        _ani = GetComponent<Animator>();
    }
    public override void HandleSkillUse()
    {
        if (_skillEnum == Skills.Emp)
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
        transform.localScale = new Vector3(EmpRaudius * 2, EmpRaudius * 2, EmpRaudius * 2);
        _ani.SetTrigger("Emp");
        StartCoroutine(SetScaleDefault());
    }

    private IEnumerator SetScaleDefault()
    {
        yield return new WaitForSeconds(0.9f);
        transform.localScale = new Vector3(1, 1, 1);
    }

    private void UltAttack()
    {
        Collider2D[] enemys = Physics2D.OverlapCircleAll(transform.position, EmpRaudius, WhatisEnemy);

        foreach (var enemy in enemys)
        {
            enemy.GetComponent<LivingEntity>().isPanic = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, EmpRaudius);
    }
}
