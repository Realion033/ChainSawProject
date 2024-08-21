using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Knife : LivingEntity
{
    private StateMachine stateMachine;
    public Transform playerTransform;

    void Awake()
    {
        stateMachine = GetComponent<StateMachine>();
        stateMachine.playerTransform = playerTransform;

        health = 120;
    }

    private void Update()
    {
        Debug.Log(health);
    }
}
