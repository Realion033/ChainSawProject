using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_PlayerHealth : MonoBehaviour
{
    public int health = 100;

    public void TakeDamage(int damage)
    {
        health -= damage;
        //Debug.Log("Player Health: " + health);
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // �÷��̾� ��� ����
        //Debug.Log("Player Died");
        // �ʿ信 ���� �߰� ��� ó�� ������ ����
    }
}
