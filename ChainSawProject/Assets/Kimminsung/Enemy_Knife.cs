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

        // 플레이어 트랜스폼을 StateMachine에 할당
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
        // 적 사망 로직 구현
        Debug.Log("Enemy Died");
        Destroy(gameObject);
    }

}
