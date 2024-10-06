using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GroundCheck : MonoBehaviour
{
    public Collision2D GroundChecker;
    public LayerMask GroundLayer;
    private Rigidbody2D rb;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == GroundLayer)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
    }
}
