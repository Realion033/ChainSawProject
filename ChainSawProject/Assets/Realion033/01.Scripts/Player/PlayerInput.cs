using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Vector2 moveVelocity;

    void Update()
    {
        ProcessInput();
        DashInput();
    }

    private void DashInput()
    {
        Input.GetKeyDown(KeyCode.Space);
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
