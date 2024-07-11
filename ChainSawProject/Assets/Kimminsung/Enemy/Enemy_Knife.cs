using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_Knife : MonoBehaviour
{
    private StateMachine stateMachine;
    public Transform playerTransform;
    [SerializeField] private float health = 100;

    void Awake()
    {
        stateMachine = GetComponent<StateMachine>();

        // 플레이어 트랜스폼을 StateMachine에 할당
        stateMachine.playerTransform = playerTransform;
    }
}
