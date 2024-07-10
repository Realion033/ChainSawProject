using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_PlayerHealth : MonoBehaviour
{
    public int health = 100;

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Player Health: " + health);
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // 플레이어 사망 로직
        Debug.Log("Player Died");
        // 필요에 따라 추가 사망 처리 로직을 구현
    }
}
