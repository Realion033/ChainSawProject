using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
public class GiantChange : Skill
{
    public float increadeValue;
    public float changeTime;

    //------not setting-------
    public Transform player;
    public PlayerAttack _playerAttack;
    private float defaultRadius;

    public override void Init()
    {
        base.Init();
        _playerInput.isSkillUse += HandleSkillUse;

        defaultRadius = _playerAttack.damageCasterRadius;
    }

    public override void HandleSkillUse()
    {
        if (_skillEnum == Skills.GiantChange)
        {
            if (PlayerCooldownManager.Instance.UseUlt())
            {
                StartCoroutine(startSkill());
            }
        }
    }

    private IEnumerator startSkill()
    {
        Giant();
        yield return new WaitForSeconds(changeTime);
        backGiant();
    }


    private void Giant()
    {
        player.transform.localScale = new Vector3(increadeValue, increadeValue, increadeValue);
        _playerAttack.damageCasterRadius = increadeValue - (increadeValue / 2);
    }
    private void backGiant()
    {
        player.transform.localScale = new Vector3(1, 1, 1);
        _playerAttack.damageCasterRadius = defaultRadius;
    }
}
