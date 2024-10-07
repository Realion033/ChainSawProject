using System.Collections;
using UnityEngine;

public class RoyalEnemy : TestEnemy
{
    public float jumpHeight = 5f; // ���� ����
    public float fallSpeed = 10f; // ���� �ӵ�
    public float attackCooldown = 7f; // ���� ��Ÿ��
    public float attackRange = 5f; // ���� ����
    public float chaseSpeed = 3f; // ���� �ӵ�
    public ParticleSystem deathParticles; // ���� �� ����� ��ƼŬ �ý���
    public float damage = 10f;

    private Animator animator;
    private Transform player; // �÷��̾��� ��ġ�� �����ϱ� ���� ����
    private Rigidbody2D rb; // Rigidbody2D ����
    private float nextAttackTime = 0f; // ���� ���ݱ��� ���� �ð�
    private bool isAttacking = false; // ���� ������ ���θ� ��Ÿ���� ����

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D ������Ʈ
        player = GameObject.FindGameObjectWithTag("KPlayer").transform; // �÷��̾� ���� ������Ʈ ã��
        health = 100f; // RoyalEnemy�� ü��
        maxHealth = health; // �ִ� ü�� ����
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isDead) return;

        if (health <= 0)
        {
            Die();
            return;
        }

        float distanceToPlayer = Mathf.Abs(player.position.x - transform.position.x); // �÷��̾���� �Ÿ� ���

        Vector2 direction = new Vector2(player.position.x - transform.position.x, 0).normalized;
        rb.velocity = new Vector2(direction.x * chaseSpeed, rb.velocity.y);

        if (distanceToPlayer > attackRange)
        {
            ChasePlayer();
        }
        else if (distanceToPlayer <= attackRange && Time.time >= nextAttackTime)
        {
            StartCoroutine(PerformJumpAttack());
        }
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

    private IEnumerator PerformJumpAttack()
    {
        isAttacking = true;
        animator.SetBool("RoyalRun", false);
        animator.SetBool("RoyalIdle", true);

        Vector2 attackDirection = new Vector2(player.position.x - transform.position.x, 0).normalized;
        rb.velocity = new Vector2(attackDirection.x, 0) * fallSpeed;

        yield return new WaitForSeconds(0.3f);

        rb.velocity = Vector2.zero;
        nextAttackTime = Time.time + attackCooldown;

        isAttacking = false;
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.collider.CompareTag("KPlayer") && isAttacking)
        {
            Player player = other.collider.GetComponent<Player>();

            if (player != null)
            {
                player.TakeHit(damage, transform.position);
            }
        }
    }

    public override void DieEffect()
    {
        base.DieEffect();

        if (deathParticles != null)
        {
            Instantiate(deathParticles, transform.position, Quaternion.identity);
        }

        StartCoroutine(DieAndPause());
        player.GetComponent<LivingEntity>().health = player.GetComponent<Player>()._playerStat.playerHealth;
    }

    private IEnumerator DieAndPause()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(0.16f);
        Time.timeScale = 1;
        Destroy(gameObject);
    }

    public override void TakeHit(float damage, Vector2 hitPos)
    {
        base.TakeHit(damage, hitPos);

        if (health <= 0)
        {
            DieEffect();
        }
    }
}