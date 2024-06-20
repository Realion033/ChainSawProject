using System;
using UnityEngine;

public class PlayerDash : PlayerInput
{
    [SerializeField] private float dashDistance = 5f; 
    [SerializeField] private float dashSpeed = 20f;   

    private Rigidbody2D rb;
    private Vector2 dashTarget;
    private bool isDashing = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isDashing)
        {
            CheckDashTargetReached();
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {   
            Dash();
        }
    }


    private void Dash()
    {
        Vector2 direction = (dashTarget - rb.position).normalized;
        Debug.Log("adsf");
        rb.velocity = direction * dashSpeed;
    }

    private void CheckDashTargetReached()
    {

        if (Vector2.Distance(rb.position, dashTarget) <= 0.5f)
        {
            isDashing = false;
            rb.velocity = Vector2.zero;
        }
    }

    public void StartDash(Vector3 mousePos)
    {
        mousePos.z = 0;  // Z 좌표는 무시

        Vector2 direction = (mousePos - transform.position).normalized;

        dashTarget = (Vector2)transform.position + direction * dashDistance;
        isDashing = true;  // 대쉬 시작
    }
}
