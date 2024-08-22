using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCooldownManager : MonoSingleton<PlayerCooldownManager>
{
    public PlayerCooldownSO playerCooldownSO;

    public float _dashCool;
    private float _attackEnergeCool;
    private float _attackTick;
    private float _ultmateCool;

    private void Start()
    {
        _dashCool = playerCooldownSO.DashCoolDown;
        _attackEnergeCool = playerCooldownSO.AttackMaxEnergyCoolDown;
        _attackTick = playerCooldownSO.AttackTick;
        _ultmateCool = playerCooldownSO.UltmateSkill;
    }
    private void Update()
    {
        CooltimeManage();
    }

    private void CooltimeManage()
    {  
        if (_dashCool != playerCooldownSO.DashCoolDown)
        {
            _dashCool += Time.deltaTime;
        }
        //else if(_dashCool > )
    }

    public bool UseDash()
    {
        if (_dashCool == playerCooldownSO.DashCoolDown)
        {
            _dashCool = 0;
            return true;
        }
        return false;
    }
    private bool AttackEnerge()
    {
        throw new NotImplementedException();
    }
    private bool AttackTick()
    {
        throw new NotImplementedException();
    }

}
