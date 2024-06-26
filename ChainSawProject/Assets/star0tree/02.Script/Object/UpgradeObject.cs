using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeObject : MonoBehaviour
{
    private GameObject UpGradeUI;
    private void Awake()
    {
        UpGradeUI = GameObject.Find("UpGradeUI");
        gameObject.SetActive(false);
    }

    public void UpGeadeAwake()
    {
        gameObject.SetActive(true);
        // �ִϸ��̼� �־��ֱ�
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") && Input.GetKeyDown(KeyCode.F))
        {
            UpGradeUI.gameObject.SetActive(true);
        }
    }
}
