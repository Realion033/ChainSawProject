using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeObject : MonoBehaviour
{
    [SerializeField] GameObject UpGradeUI;
    private bool onCollision; 
    private void Awake()
    {
        //gameObject.SetActive(false);
    }

    public void UpGeadeAwake()
    {
        gameObject.SetActive(true);
        // 애니메이션 넣어주기
    }

    private void Update()
    {
        if (onCollision && Input.GetKeyDown(KeyCode.F))
        {
            UpGradeUI.gameObject.SetActive(true);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            onCollision = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            onCollision = false;
        }
    }
}
