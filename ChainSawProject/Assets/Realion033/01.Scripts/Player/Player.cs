using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f;

    public float dashDistance = 5f;
    public float dashSpeed = 20f;

    private PlayerInput playerInput;
    private PlayerMovement playerMovement;
    private PlayerDash playerDash;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();
        playerDash = GetComponent<PlayerDash>();
    }

    void Update()
    {
        Move();
        Dash();
    }

    private void Dash()
    {
        if(playerInput.isDash == true)
        {
            playerDash.StartDash(playerInput.mousePos);
        }
    }

    private void Move()
    {
        Vector2 moveVelocity = playerInput.GetMoveVelocity();
        playerMovement.Move(moveVelocity * speed);
    }
}
