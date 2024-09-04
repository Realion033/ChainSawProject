using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : LivingEntity
{
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        health = 300;
    }
    private void Update()
    {
        Debug.Log("current health" + health);
    }

    public override void TakeHit(float damage, Vector2 hitPos)
    {
        base.TakeHit(damage, hitPos);
        StartCoroutine(FlashRed());
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
}
