using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    //this script well base any skill scripts
    private PlayerInput _playerInput;
    private PlayerCooldownSO _playerCooldownSO;
    public virtual void Init()
    {
        _playerInput = GetComponentInParent<PlayerInput>();
        _playerCooldownSO = new PlayerCooldownSO();
    }

    private void Awake()
    {
        Init();
    }
    void Start()
    {
        _playerInput.isSkillUse += HandleSkillUse;
    }

    public virtual void HandleSkillUse()
    {

    }
}
