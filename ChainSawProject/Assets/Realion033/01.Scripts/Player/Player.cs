using System;
using System.Collections;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class Player : LivingEntity
{
    public PlayerStatSO _playerStat;

    private SpriteRenderer _spriteRenderer;
    private CinemachineImpulseSource _source;
    private PlayerInput playerInput;
    private PlayerMovement playerMovement;
    private PlayerDash playerDash;

    private void Start()
    {
        health = _playerStat.playerHealth;
    }

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();
        playerDash = GetComponent<PlayerDash>();

        Transform visualTrm = transform.Find("Visual");
        _spriteRenderer = visualTrm.GetComponent<SpriteRenderer>();

        _source = GetComponent<CinemachineImpulseSource>();
    }

    void Update()
    {
        Move();
        Dash();
        if(Input.GetKeyDown(KeyCode.O)){
            TakeHit(1, Vector2.zero);
        }
    }

    public override void TakeHit(float damage, Vector2 hitPos)
    {
        base.TakeHit(damage, hitPos);
        StartCoroutine(FlashRed());
        _source.GenerateImpulse();
    }

    private IEnumerator FlashRed()
    {
        _spriteRenderer.color = Color.red;

        float duration = 0.5f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            _spriteRenderer.color = Color.Lerp(Color.red, Color.white, elapsed / duration);
            yield return null;
        }

        _spriteRenderer.color = Color.white;
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
        playerMovement.Move(moveVelocity * _playerStat.speed);
    }
}
