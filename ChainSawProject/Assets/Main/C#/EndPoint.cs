using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    private MainGameManager _mainGameManager;

    private void Awake()
    {
        _mainGameManager = GameObject.Find("GameOwner").GetComponent<MainGameManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("KPlayer"))
        {
            if (_mainGameManager.CurrentLevel >= 4)
            {
                _mainGameManager.Thanks();
            }
            _mainGameManager.CurrentLevel++;
            _mainGameManager.GameOver();
        }
    }
}