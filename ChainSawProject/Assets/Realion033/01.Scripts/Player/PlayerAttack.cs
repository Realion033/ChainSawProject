using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (_playerInput.isSlash)
        {
            LivingEntity livingEntity = other.gameObject.GetComponent<LivingEntity>();
            livingEntity.TakeHit(30, Vector2.zero);
        }
    }
}
