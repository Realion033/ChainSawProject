using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class TestEnemy : LivingEntity
{
    [SerializeField] GameObject f;
    [SerializeField] ParticleSystem _blood;
    private SpriteRenderer _spriteRenderer;
    private CapsuleCollider2D _collider2;
    private Slider _slider;
    public float maxHealth;
    private int cnt = 0;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider2 = GetComponent<CapsuleCollider2D>();
        _slider = GetComponentInChildren<Slider>();
        maxHealth = 50;
        health = 50;
    }
    private void Update()
    {
        _slider.value = health / maxHealth;

        if (isDead)
        {
            f.SetActive(false);
        }
        else
        {
            f.SetActive(true);
        }
    }

    public override void TakeHit(float damage, Vector2 hitPos)
    {
        base.TakeHit(damage, hitPos);
        if (!isDead)
        {
            Instantiate(_blood, hitPos, Quaternion.identity);
            StartCoroutine(FlashRed());
        }
        if (isDead && cnt == 0)
        {
            Instantiate(_blood, hitPos, Quaternion.identity);
            cnt++;
        }
    }

    public override void Panic(float panicTime)
    {
        
        base.Panic(panicTime);
    }
    private IEnumerator FlashRed()
    {
        _spriteRenderer.color = Color.red;

        float duration = 0.1f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            _spriteRenderer.color = Color.Lerp(Color.red, Color.white, elapsed / duration);
            yield return null;
        }

        _spriteRenderer.color = Color.white;
    }

    public override void DieEffect()
    {
        StartCoroutine(Dieef());
    }

    private IEnumerator Dieef()
    {
        _collider2.isTrigger = true;
        // 처음에 빨간색으로 변환
        _spriteRenderer.color = new Color(1f, 0f, 0f, 1f); // 빨간색, 알파값 1 (불투명)

        float fadeDuration = 0.2f; // 페이드 아웃이 걸리는 시간 (초)
        float fadeSpeed = 1f / fadeDuration; // 페이드 아웃 속도

        for (float t = 0; t < 1; t += Time.deltaTime * fadeSpeed)
        {
            Color newColor = _spriteRenderer.color;
            newColor.a = Mathf.Lerp(1f, 0f, t); // 알파값을 1에서 0으로 서서히 줄임
            _spriteRenderer.color = newColor;
            yield return null;
        }
        // 완전히 투명해지면 오브젝트를 비활성화하거나 제거
        _spriteRenderer.color = new Color(1f, 0f, 0f, 0f); // 완전히 투명한 상태
    }

    public override IEnumerator waitDieEffect()
    {
        yield return new WaitForSeconds(2f);
        _collider2.isTrigger = false;
        health = 50;
        _spriteRenderer.color = new Color(1f, 0f, 0f, 1f);
        _spriteRenderer.color = Color.white;
        isDead = false;
        cnt = 0;
    }
}
