using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Knife : MonoBehaviour
{
    private StateMachine stateMachine;
    public Transform playerTransform;
    public int health = 120;

    void Awake()
    {
        stateMachine = GetComponent<StateMachine>();

        // �÷��̾� Ʈ�������� StateMachine�� �Ҵ�
        stateMachine.playerTransform = playerTransform;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // �� ��� ���� ����
        Debug.Log("Enemy Died");
        Destroy(gameObject);
    }

}
