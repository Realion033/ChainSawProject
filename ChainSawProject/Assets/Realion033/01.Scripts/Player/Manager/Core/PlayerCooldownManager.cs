using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCooldownManager : MonoSingleton<PlayerCooldownManager>
{
    public PlayerCooldownSO playerCooldownSO;

    public float _dashCool;
    public float _attackEnergeCool;
    public float _attackTick;
    public float _ultmateCool;

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
        //dash cool
        if (_dashCool < playerCooldownSO.DashCoolDown)
        {
            _dashCool += Time.deltaTime;
        }
        else if (_dashCool >= playerCooldownSO.DashCoolDown)
        {
            _dashCool = playerCooldownSO.DashCoolDown;
        }

        //attackTick
        if (_attackTick < playerCooldownSO.AttackTick)
        {
            _attackTick += Time.deltaTime;
        }
        else if (_attackTick >= playerCooldownSO.AttackTick)
        {
            _attackTick = playerCooldownSO.AttackTick;
        }

        //ultcool
        if (_ultmateCool < playerCooldownSO.UltmateSkill)
        {
            _ultmateCool += Time.deltaTime;
        }
        else if (_ultmateCool >= playerCooldownSO.UltmateSkill)
        {
            _ultmateCool = playerCooldownSO.UltmateSkill;
        }
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
    public bool AttackTick()
    {
        if (_attackTick == playerCooldownSO.AttackTick)
        {
            _attackTick = 0;
            return true;
        }
        return false;
    }
    private bool AttackEnerge()
    {
        throw new NotImplementedException();
    }

    public bool UseUlt()
    {
        if (_ultmateCool == playerCooldownSO.UltmateSkill)
        {
            _ultmateCool = 0;
            return true;
        }
        return false;
    }

}
