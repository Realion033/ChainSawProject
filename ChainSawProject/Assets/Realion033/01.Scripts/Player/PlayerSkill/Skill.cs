using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Skills
{
    None,
    FinalAttack,
    RocketLuncer,
    GiantChange,
    Giman,
    Emp
}

public class Skill : MonoBehaviour
{
    //this script well base any skill scripts
    public Skills _skillEnum;
    protected PlayerInput _playerInput;
    protected PlayerCooldownSO _playerCooldownSO;
    public virtual void Init()
    {
        _playerInput = GetComponentInParent<PlayerInput>();
        _playerCooldownSO = new PlayerCooldownSO();

        _skillEnum = Skills.GiantChange;
    }

    private void Awake()
    {
        Init();
    }

    public virtual void HandleSkillUse()
    {

    }
}
