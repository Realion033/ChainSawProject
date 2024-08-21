using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    Animator ani;
    PlayerInput pi;

    private void Awake()
    {
        ani = GetComponent<Animator>();
        pi = GetComponentInParent<PlayerInput>();
    }

    private void Update()
    {
        SlashAni();
        TowardRotateAni();
    }

    private void TowardRotateAni()
    {
        
    }

    private void SlashAni()
    {
        if (pi.isSlash)
        {
            ani.SetBool("IsSlash", true);
        }
        if (!pi.isSlash)
        {
            ani.SetBool("IsSlash", false);
        }
    }
}
