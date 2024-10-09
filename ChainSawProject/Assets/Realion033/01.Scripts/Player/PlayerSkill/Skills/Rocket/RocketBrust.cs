using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBrust : RocketLuncher
{
    private Rigidbody2D _rb;
    private RocketLuncher _rl;
    private Animator _ani;
    private float time = 0;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _ani = GetComponentInChildren<Animator>();
        _rl = FindObjectOfType<RocketLuncher>();
    }

    private void Update()
    {
        time += Time.deltaTime;

        if (time > 3)
        {
            DestroyEffect();
        }
    }

    private void DestroyEffect()
    {
        _rb.simulated = false;
        _ani.SetBool("Exo", true);
        GiveDmg();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            _rb.simulated = false;
            _ani.SetBool("Exo", true);
            GiveDmg();
        }
    }

    private void GiveDmg()
    {
        Collider2D[] enemys = Physics2D.OverlapCircleAll(transform.position, 2.5f, WhatisEnemy);

        foreach (var enemy in enemys)
        {
            LivingEntity livingEntity = enemy.GetComponent<LivingEntity>();
            if (livingEntity != null)
            {
                livingEntity.TakeHit(LuncherDamage, enemy.transform.position);
            }
            else
            {
                Debug.LogWarning($"No LivingEntity component found on {enemy.gameObject.name}");
            }
        }

        Debug.Log(enemys.Length); // 배열 길이 출력
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 2.5f);
    }
}
