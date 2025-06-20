using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LivingEntity : MonoBehaviour, IDamageable_real
{
    public float health;
    public bool isDead = false;
    public bool isPanic = false;

    private float defultHealth;

    void Awake()
    {
        defultHealth = health;
    }

    public virtual void TakeHit(float damage, Vector2 hitPos)
    {
        if (health == defultHealth)
        {
            //StartCoroutine(HitStopCoroutine(0.2f, 0.33f));
        }

        health -= damage;

        if (health <= 0 & !isDead)
        {
            Die();
            DieEffect();
        }
    }
    public virtual void Die()
    {
        isDead = true;
        //StartCoroutine(HitStopCoroutine(0f, 0.33f)); // (완전 정지 시간, 느린 상태 유지, 복구 시간)
        DieEffect();
        StartCoroutine(waitDieEffect());
    }

    // private IEnumerator HitStopCoroutine(float slowDuration, float recoveryTime)
    // {
    //     Time.timeScale = 0f; // 매우 느린 상태로 전환 (0.2배속)
    //     yield return new WaitForSecondsRealtime(slowDuration); // 느린 상태 유지

    //     float elapsed = 0f;
    //     while (elapsed < recoveryTime)
    //     {
    //         elapsed += Time.unscaledDeltaTime;
    //         Time.timeScale = Mathf.Lerp(0.2f, 1.0f, elapsed / recoveryTime); // 점진적 복구
    //         yield return null;
    //     }
    //     Time.timeScale = 1.0f; // 완전히 복구
    // }

    public virtual void DieEffect()
    {
    }

    public virtual IEnumerator waitDieEffect()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        Destroy(gameObject);
    }

    public virtual void Panic(float panicTime)
    {

    }
}
