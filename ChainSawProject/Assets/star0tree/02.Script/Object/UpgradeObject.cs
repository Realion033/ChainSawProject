using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeObject : MonoBehaviour
{
    [SerializeField] GameObject UpGradeUI;
    private void Awake()
    {
        //gameObject.SetActive(false);
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
