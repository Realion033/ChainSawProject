﻿using System.Collections;
using UnityEngine;

public class RoyalEnemy : LivingEntity
{
    public float jumpHeight = 5f; // ���� ����
    public float fallSpeed = 10f; // ���� �ӵ�
    public float attackCooldown = 7f; // ���� ��Ÿ��
    public float attackRange = 10f; // ���� ����
    public float chaseSpeed = 3f; // ���� �ӵ�
    public ParticleSystem deathParticles; // ���� �� ������ ��ƼŬ �ý���
    public float damage = 10f;
    public float maxHealth;

    private Animator animator;
    private Transform player ; // �÷��̾��� ��ġ�� �����ϱ� ���� ����
    private Rigidbody2D rb; // Rigidbody2D ����
    private float nextAttackTime = 0f; // ���� ���ݱ��� ���� �ð�
    private bool isAttacking = false; // ���� ������ ���θ� ��Ÿ���� ����

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D 占쏙옙占쏙옙占쏙옙트 占쏙옙占쏙옙占쏙옙占쏙옙
        player = GameObject.FindGameObjectWithTag("KPlayer").transform; // 占시뤄옙占싱억옙 占쏙옙占쏙옙占쏙옙트 찾占쏙옙
        health = 100f; // RoyalEnemy占쏙옙 占십깍옙 체占쏙옙
        maxHealth = health; // 占쌍댐옙 체占쏙옙 占쏙옙占쏙옙

        animator = GetComponent<Animator>();
    }


    private void Update()
    {
        if (isDead) return; // 占쏙옙占쏙옙 占쏙옙占쏙옙占쏙옙 占싣뱄옙占싶듸옙 占쏙옙 占쏙옙


        if (health <= 0)
        {
            Die();
            return; // 占쌓억옙占쏙옙 占쏙옙占쏙옙 占쏙옙 占싱삼옙 占쏙옙占쏙옙占쏙옙占쏙옙 占쏙옙占쏙옙
        }

        float distanceToPlayer = Mathf.Abs(player.position.x - transform.position.x); // X占쏙옙 占신몌옙 占쏙옙占?

        // 占시뤄옙占싱억옙占쏙옙 占쏙옙치占쏙옙占쏙옙 X 占쏙옙표占쏙옙 占쏙옙占쏙옙構占? Y占쏙옙 占쏙옙占쏙옙 占쏙옙占쏙옙 Y 占쏙옙치占쏙옙 占쏙옙占쏙옙
        Vector2 direction = new Vector2(player.position.x - transform.position.x, 0).normalized;

        // 占쏙옙占쏙옙 占쌈듸옙占쏙옙큼 X占쏙옙占쏙옙占싸몌옙 占싱듸옙 (Y占쏙옙占쏙옙 0占쏙옙占쏙옙 占쏙옙占쏙옙占쏙옙)
        rb.velocity = new Vector2(direction.x * chaseSpeed, rb.velocity.y);

        // 占시뤄옙占싱어를 占쏙옙占쏙옙占싹댐옙 占쏙옙占쏙옙 占쏙옙
        return;
    }


    
    private void ChasePlayer()
    {
        Vector2 direction = new Vector2(player.position.x - transform.position.x, 0).normalized;
        if (direction.x > 0.3f)
        {
            rb.velocity = new Vector2(direction.x * chaseSpeed, rb.velocity.y);

            animator.SetBool("RoyalRun", true);
            animator.SetBool("RoyalIdle", false);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    // 占쏙옙占쏙옙 占쏙옙占쏙옙 占쏙옙占쏙옙
    private IEnumerator PerformJumpAttack()
    {

        isAttacking = true;
        animator.SetBool("RoyalRun", true);
        animator.SetBool("RoyalIdle", false);

        Vector2 attackDirection = new Vector2(player.position.x - transform.position.x, jumpHeight).normalized;

        rb.velocity = new Vector2(attackDirection.x * chaseSpeed, jumpHeight);
    
        yield return new WaitForSeconds(0.5f);
        float xMovement = 20f;

        rb.velocity = new Vector2(xMovement * Mathf.Sign(attackDirection.x), -fallSpeed);

        yield return new WaitForSeconds(0.1f);

        rb.velocity = Vector2.zero; // 占쌈듸옙 0占쏙옙占쏙옙 占쏙옙占쏙옙占싹울옙 占쏙옙占쏙옙 占쏙옙 占쏙옙占쏙옙
        nextAttackTime = Time.time + attackCooldown; // 占쏙옙占쏙옙 占쏙옙타占쏙옙 占쏙옙占쏙옙
    }


    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.collider.CompareTag("KPlayer"))
        {
            Player player = other.collider.GetComponent<Player>(); // Player 占쏙옙크占쏙옙트 占쏙옙占쏙옙

            if (player != null)
            {
                player.TakeHit(damage, transform.position); // 占시뤄옙占싱어에占쏙옙 占쏙옙占쏙옙 占쏙옙占쏙옙
            }
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("KPlayer"))
        {
            Player player = other.GetComponent<Player>(); // Player 占쏙옙크占쏙옙트 占쏙옙占쏙옙

            if (player != null)
            {
                player.TakeHit(damage, transform.position); // 占시뤄옙占싱어에占쏙옙 占쏙옙占쏙옙 占쏙옙占쏙옙
            }
        }
    }

        // 占쏙옙티클 占시쏙옙占쏙옙 占쏙옙占쏙옙
    public override void DieEffect()
    {
        base.DieEffect();

        if (deathParticles != null)
        {
            Instantiate(deathParticles, transform.position, Quaternion.identity);
        }

        // 占쌘뤄옙틴 占쏙옙占쏙옙: Time.timeScale 0 -> 占쏙옙占?-> Time.timeScale 1 占쏙옙占쏙옙
        //StartCoroutine(DieAndPause());

        player.GetComponent<LivingEntity>().health = player.GetComponent<Player>()._playerStat.playerHealth;
    }

    // 占쌘뤄옙틴: 占쏙옙占쏙옙 占쏙옙 타占쌈쏙옙占쏙옙占쏙옙 占쏙옙占쏙옙 占쏙옙 占쏙옙占쏙옙 占시곤옙 占쏙옙占?
    private IEnumerator DieAndPause()
    {
        Time.timeScale = 0; // 占쏙옙占쏙옙 占싹쏙옙占쏙옙占쏙옙
        yield return new WaitForSecondsRealtime(0.16f); // 占쏙옙占쏙옙 占시곤옙占쏙옙占쏙옙 0.1占쏙옙 占쏙옙占?
        Time.timeScale = 1; // 占쌕쏙옙 占쏙옙占쏙옙 占썹개
        Destroy(gameObject); // 占쏙옙 占쏙옙占쏙옙占쏙옙트 占쏙옙占쏙옙
    }

    public override void TakeHit(float damage, Vector2 hitPos)
    {
        base.TakeHit(damage, hitPos);

        // 占쏙옙占쏙옙 占쌓억옙占쏙옙 占쏙옙 처占쏙옙
        if (health <= 0)
        {
            DieEffect(); // 占쏙옙占?占쏙옙占쏙옙트 호占쏙옙
        }

    }
}