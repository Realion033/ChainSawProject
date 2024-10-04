using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
public class GiantChange : Skill
{
    public float increadeValue;
    public float changeTime;
    public float increadeDamage;
    public float increadeShield;

    //------not setting-------
    [Header("직접 처넣어 ㅅㅂ")]
    public Transform player;
    public PlayerAttack _playerAttack;
    public PlayerStatSO _playerStat;
    private float defaultRadius;
    private bool _isTimer = false;
    private float _time = 0;

    private Animator _ani;

    public override void Init()
    {
        base.Init();
        _playerInput.isSkillUse += HandleSkillUse;

        defaultRadius = _playerAttack.damageCasterRadius;
        _ani = GetComponent<Animator>();
    }

    public override void HandleSkillUse()
    {
        if (_skillEnum == Skills.GiantChange)
        {
            if (PlayerCooldownManager.Instance.UseUlt())
            {
                Giant();
                _isTimer = true;
            }
        }
    }

    private void Update()
    {
        if (_isTimer)
        {
            _time += Time.deltaTime;

            if (_time > changeTime)
            {
                backGiant();
                _isTimer = false;
                _time = 0;
            }
        }

        Debug.Log(_playerStat.playerDamage);
    }

    private void Giant()
    {
        player.transform.localScale = new Vector3(increadeValue, increadeValue, increadeValue);
        _playerAttack.damageCasterRadius = increadeValue / 2;

        _playerStat.playershield += increadeShield;
        _playerStat.playerDamage += increadeDamage;
        
        if (_time < 0.1f)
        {
            _ani.SetTrigger("GiantChange");
        }
    }
    private void backGiant()
    {
        player.transform.localScale = new Vector3(1, 1, 1);
        _playerAttack.damageCasterRadius = defaultRadius;

        _playerStat.playershield = 0;
        _playerStat.playerDamage = increadeDamage;
    }
}
