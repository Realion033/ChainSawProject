using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBrust : MonoBehaviour
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
        _ani.SetBool("Exo", true);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            _ani.SetBool("Exo", true);
        }
    }
}
