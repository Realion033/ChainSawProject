using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_Knife : MonoBehaviour
{
    private StateMachine stateMachine;
    public Transform playerTransform;
    public Animator anim { get; private set; }
    [SerializeField] private float health = 100;

    void Awake()
    {
        stateMachine = GetComponent<StateMachine>();
        anim = GetComponent<Animator>();

        // �÷��̾� Ʈ�������� StateMachine�� �Ҵ�
        stateMachine.playerTransform = playerTransform;
    }
}
