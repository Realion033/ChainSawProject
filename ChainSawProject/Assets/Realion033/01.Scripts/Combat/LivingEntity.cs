using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable
{
    protected float health;
    public bool isDead;
    public virtual void TakeHit(float damage, Vector2 hitPos)
    {
        health -= damage;

        if(health <= 0 &!isDead){
            Die();
        }
    }
    protected void Die()
    {
        isDead = true;
        Destroy(gameObject);
    }
}
