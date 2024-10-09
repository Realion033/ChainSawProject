using System;
using System.Collections;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class PlayerDash : PlayerInput
{
    private Player player;
    private TrailRenderer _trailRender;
    public Rigidbody2D rb;
    public Vector2 dashTarget;
    public Vector2 direction;
    public bool isDashing = false;

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
        if (!isDashing)
        {
            dashTarget = transform.position;
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


    public void StartDash(Vector3 mousePos)
    {
        mousePos.z = 0;  // 아아아

        direction = (mousePos - transform.position).normalized;

        dashTarget = (Vector2)transform.position + direction * player._playerStat.dashDistance;
        isDashing = true;  // ?�� ????
    }

    //stop Dash case
    private void CheckDashTargetReached()
    {

        if (Vector2.Distance(rb.position, dashTarget) <= 0.5f)
        {
            isDashing = false;
            rb.velocity = Vector2.zero;
            StartCoroutine(TrailDelay());
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        isDashing = false;
        rb.velocity = Vector2.zero;
        StartCoroutine(TrailDelay());
    }

    private IEnumerator TrailDelay()
    {
        yield return new WaitForSeconds(0.21f);
        _trailRender.time = 0.09f;
    }
}
