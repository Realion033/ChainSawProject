using System.Collections;
using UnityEngine;

public class ShildEnemy : LivingEntity
{
    public float jumpHeight = 5f; // ���� ����
    public float fallSpeed = 10f; // ���� �ӵ�
    public float attackCooldown = 7f; // ���� ��Ÿ��
    public float attackRange = 10f; // ���� ����
    public float chaseSpeed = 3f; // ���� �ӵ�
    public ParticleSystem deathParticles; // ���� �� ����� ��ƼŬ �ý���
    public float damage = 10f;
    public float maxHealth;

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

        // ��ٿ� üũ �߰�
        if (Time.time < nextAttackTime)
        {
            // ��ٿ� ���� �÷��̾ ������ �ϵ��� ����
            Vector2 direction = new Vector2(player.position.x - transform.position.x, 0).normalized;
            rb.velocity = new Vector2(direction.x * chaseSpeed, rb.velocity.y);
            return;
        }

        Vector2 chaseDirection = new Vector2(player.position.x - transform.position.x, 0).normalized;

        if (distanceToPlayer > attackRange)
        {
            ChasePlayer();
        }
        else if (distanceToPlayer <= attackRange)
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

            animator.SetBool("SheildRun", true);
            animator.SetBool("SheildIdle", false);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private IEnumerator PerformJumpAttack()
    {
        if (isAttacking) yield break; // �̹� ���� ���̸� ����

        isAttacking = true;
        animator.SetBool("SheildRun", true);
        animator.SetBool("SheildIdle", false);

        // ���� ���� ���
        Vector2 attackDirection = new Vector2(player.position.x - transform.position.x, 0).normalized;

        // X�����θ� �̵��ϱ� ���� �ӵ� ����
        float xMovement = 10f;
        rb.velocity = new Vector2(xMovement * Mathf.Sign(attackDirection.x), 0);

        // X������ �̵� ����
        yield return new WaitForSeconds(0.5f);

        // ���� ���� �� ���� ����
        rb.velocity = Vector2.zero;
        nextAttackTime = Time.time + attackCooldown; // ��ٿ� �ð� ����

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

        //StartCoroutine(DieAndPause());
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
