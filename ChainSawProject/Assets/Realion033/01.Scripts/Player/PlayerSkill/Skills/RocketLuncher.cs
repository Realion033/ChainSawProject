using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLuncher : Skill
{
    [SerializeField] private LayerMask WhatisEnemy;
    [SerializeField] private GameObject Rocket;
    public float Damage = 150f;
    public float Distace = 3f;

    public override void Init()
    {
        base.Init();
        _playerInput.isSkillUse += HandleSkillUse;
    }

    public override void HandleSkillUse()
    {
        if (_skillEnum == Skills.RocketLuncer)
        {
            if (PlayerCooldownManager.Instance.UseUlt())
            {
                RocketAttack();
                Debug.Log("ULT on!");
            }
        }
    }

    private void RocketAttack()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.WorldToViewportPoint(mousePosition);

        Debug.Log(mousePosition);
    }
}
