using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FakePlayer : MonoBehaviour
{
    private GameObject InputGuide;

    public event Action OnJumpKeyPress;

    public Action onInputF;
    void Awake()
    {
        InputGuide = GameObject.Find("InputGuide");
        InputGuide.SetActive(false);
    }

    private void HandleJumpKeyPress()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            OnJumpKeyPress?.Invoke();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        InputGuide.SetActive(true);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        InputGuide.SetActive(false);
    }
}
