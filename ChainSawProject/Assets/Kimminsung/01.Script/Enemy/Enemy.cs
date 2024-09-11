using MIN;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Agent
{
    [Header("Common settings")]
    public float moveSpeed;
    public float battleTime;
    public bool isActive;

    //[field: serializefield] public droptableso droptable { get; private set; }

    protected float _defaultMoveSpeed;

    [SerializeField] protected LayerMask _whatIsPlayer;
    [SerializeField] protected LayerMask _whatIsObstacle;

    [Header("Attack Settings")]
    public float runAwayDistance;
    public float attackDistance;
    public float attackCooldown;
    [SerializeField] protected int _maxCheckEnemy = 1;
    [HideInInspector] public float lastAttackTime;
    [HideInInspector] public Transform targetTrm;
    protected Collider2D[] _enemyCheckColliders;

   

    protected override void Awake()
    {
        base.Awake();
        _defaultMoveSpeed = moveSpeed;
        _enemyCheckColliders = new Collider2D[_maxCheckEnemy];
    }

    public abstract void Run();

    public virtual Collider2D IsPlayerDetected()
    {
        int cnt = Physics2D.OverlapCircleNonAlloc(transform.position, runAwayDistance, _enemyCheckColliders, _whatIsPlayer);

        return cnt >= 1 ? _enemyCheckColliders[0] : null;
    }

    public virtual bool IsObstacleInLine(float distance, Vector3 direction)
    {
        return Physics2D.Raycast(transform.position, direction, distance, _whatIsObstacle);
    }


    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, runAwayDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
        Gizmos.color = Color.white;
    }

    public abstract void AnimationEndTrigger();
}
