using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    //this script well base any skill scripts
    private PlayerInput _playerInput;
    private PlayerCooldownSO _playerCooldownSO;
    private void Awake()
    {
        _playerInput = transform.Find("Player").GetComponent<PlayerInput>();
        _playerCooldownSO = new PlayerCooldownSO();
    }
    void Start()
    {
        _playerInput.isSkillUse += HandleSkillUse;
    }

    public virtual void HandleSkillUse()
    {

    }
}
