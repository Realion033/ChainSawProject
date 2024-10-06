using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LivingEntity : MonoBehaviour, IDamageable_real
{
    public float health;
    public bool isDead = false;
    public bool isPanic = false;
    public virtual void TakeHit(float damage, Vector2 hitPos)
    {
        health -= damage;

        if (health <= 0 & !isDead)
        {
            Die();
        }
    }
    public virtual void Die()
    {
        isDead = true;
        DieEffect();
        StartCoroutine(waitDieEffect());
    }

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
