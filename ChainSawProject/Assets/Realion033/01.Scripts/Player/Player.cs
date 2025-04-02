using System;
using System.Collections;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : LivingEntity
{
    public MainGameManager Mm;
    public PlayerStatSO _playerStat;
    public Slider _healthBar;
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
        if (health > _playerStat.playerHealth)
        {
            health = _playerStat.playerHealth;
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            TakeHit(10, Vector2.zero);
        }

        //health bar
        _healthBar.value = health / _playerStat.playerHealth;
    }

    public override void TakeHit(float damage, Vector2 hitPos)
    {
        if (_playerStat.playershield > 0)
        {
            _playerStat.playershield = _playerStat.playershield - damage;
            Debug.Log("데미지가 감소됨!");
        }
        else
        {
            base.TakeHit(damage, hitPos);
            StartCoroutine(FlashRed());
            _source.GenerateImpulse();
            StartCoroutine(HitStopCoroutine(0.2f, 0.33f));
            Debug.Log(health);
        }
    }

    private IEnumerator HitStopCoroutine(float slowDuration, float recoveryTime)
    {
        Time.timeScale = 0f; // 매우 느린 상태로 전환 (0.2배속)
        yield return new WaitForSecondsRealtime(slowDuration); // 느린 상태 유지

        float elapsed = 0f;
        while (elapsed < recoveryTime)
        {
            elapsed += Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Lerp(0.2f, 1.0f, elapsed / recoveryTime); // 점진적 복구
            yield return null;
        }
        Time.timeScale = 1.0f; // 완전히 복구
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
        if (playerInput.isDash == true)
        {
            playerDash.StartDash(playerInput.mousePos);
        }
    }

    private void Move()
    {
        Vector2 moveVelocity = playerInput.GetMoveVelocity();
        playerMovement.Move(moveVelocity * _playerStat.speed);
    }

    public override void Die()
    {
        Debug.Log("Die");
        Mm.GameOver();
    }
}
