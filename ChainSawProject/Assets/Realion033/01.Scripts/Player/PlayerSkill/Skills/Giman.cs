using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Giman : Skill
{
    public float DamageMultiple = 2f;
    public float EffectTime = 5f;

    public PlayerStatSO _playerStat;

    private Animator _ani;
    private bool _isTimer = false;
    private float _time = 0;

    private float _defaultDamage;

    public override void Init()
    {
        base.Init();
        _playerInput.isSkillUse += HandleSkillUse;

        _ani = GetComponent<Animator>();

        _defaultDamage = _playerStat.playerDamage;
    }

    public override void HandleSkillUse()
    {
        if (_skillEnum == Skills.Giman)
        {
            if (PlayerCooldownManager.Instance.UseUlt())
            {
                isGiman();
                _isTimer = true;
            }
        }
    }

    private void isGiman()
    {
        _playerStat.playerDamage = _playerStat.playerDamage * DamageMultiple;

        if (_time < 0.1f)
        {
            _ani.SetTrigger("GiantChange");
        }
    }

    private void Update()
    {
        if (_isTimer)
        {
            _time += Time.deltaTime;

            if (_time > EffectTime)
            {
                Back();
                _isTimer = false;
                _time = 0;
            }
        }
    }

    private void Back()
    {
        _playerStat.playerDamage = _defaultDamage;
    }
}
