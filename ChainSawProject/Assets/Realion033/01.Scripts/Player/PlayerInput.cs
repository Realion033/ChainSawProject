using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Vector2 moveVelocity;

    public Vector3 mousePos { get; private set; }
    public bool isDash { get; private set; } = false;
    public bool isSlash { get; private set; } = false;

    void Update()
    {
        ProcessInput();
        DashInput();
        AttackInput();
    }

    private void AttackInput()
    {
        if(Input.GetMouseButtonDown(0))
        {
            isSlash = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isSlash = false;
        }
    }

    private void DashInput()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(Input.GetKeyDown(KeyCode.Space)){
            isDash = true;
        }
        else
        {
            isDash = false;
        }
    }

    private void ProcessInput()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        moveVelocity = new Vector2(x, y);
    }

    public Vector2 GetMoveVelocity()
    {
        return moveVelocity;
    }
}
