using System;
using UnityEngine;

public class PlayerDash : PlayerInput
{
    private Player player;
    private TrailRenderer _trailRender;
    private Rigidbody2D rb;
    private Vector2 dashTarget;

    private bool isDashing = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
        _trailRender = GetComponent<TrailRenderer>();
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
        rb.velocity = direction * player._playerStat.dashSpeed;

        //start trail.
        _trailRender.time = 0.2f;
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
        mousePos.z = 0;  // Z ��ǥ�� ����

        Vector2 direction = (mousePos - transform.position).normalized;

        dashTarget = (Vector2)transform.position + direction * player._playerStat.dashDistance;
        isDashing = true;  // �뽬 ����
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        isDashing = false;
        rb.velocity = Vector2.zero;
    }
}
