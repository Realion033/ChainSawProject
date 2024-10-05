using System.Collections;
using UnityEngine;

public class RoyalEnemy : MonoBehaviour
{
    public float health = 100f; // �� ü��
    public float damage = 14f; // ���� ������
    public float jumpHeight = 5f; // ���� ����
    public float fallSpeed = 10f; // ���� �ӵ�
    public float attackCooldown = 7f; // ���� ��Ÿ��
    public float attackRange = 5f; // ���� ����
    private Transform player; // �÷��̾��� ��ġ�� �����ϱ� ���� ����
    public Animator animator; // �ִϸ����� ������Ʈ ����
    public ParticleSystem deathParticles; // ���� �� ����� ��ƼŬ �ý���

    private bool isAttacking = false;
    private bool isAlive = true;

    private Rigidbody2D rb; // Rigidbody2D ����
    private float nextAttackTime = 0f; // ���� ���ݱ��� ���� �ð�

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("KPlayer").transform;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isAlive) return; // ���� ������ �ƹ��͵� �� ��

        if (health <= 0)
        {
            Die();
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // �÷��̾ �����ϴ� ���� ��
        if (!isAttacking && Time.time >= nextAttackTime)
        {
            if (distanceToPlayer > attackRange)
            {
                ChasePlayer();
            }
            else if (distanceToPlayer <= attackRange)
            {
                StartCoroutine(PerformJumpAttack());
            }
        }
    }

    // �÷��̾ �����ϴ� �Լ�
    void ChasePlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * 3f; // ���� �ӵ��� 3
        animator.SetBool("RoyalRun", true); // RoyalRun �ִϸ��̼� ����
        animator.SetBool("RoyalIdle", false); // RoyalIdle ��Ȱ��ȭ
    }

    // ���� ���� ����
    IEnumerator PerformJumpAttack()
    {
        isAttacking = true;
        animator.SetBool("RoyalRun", false); // RoyalRun ����
        animator.SetBool("RoyalIdle", true); // RoyalIdle ��� �ִϸ��̼� ����

        // ���� ����
        rb.velocity = new Vector2(0, jumpHeight);
        yield return new WaitForSeconds(0.5f); // ���� �� ��� �ð�

        // �밢�� �Ʒ��� ������ �����ϸ� ����
        Vector2 attackDirection = (player.position - transform.position).normalized;
        rb.velocity = new Vector2(attackDirection.x, -1) * fallSpeed;

        yield return new WaitForSeconds(0.3f); // ���� �� ���

        rb.velocity = Vector2.zero; // �ӵ� 0���� �����Ͽ� ���� �� ����
        nextAttackTime = Time.time + attackCooldown; // ���� ��Ÿ�� ����
        isAttacking = false; // ���� ����
    }

    // ���� �׾��� �� ȣ��Ǵ� �Լ�
    void Die()
    {
        isAlive = false;
        animator.SetBool("RoyalDie", true); // �״� �ִϸ��̼� ����
        animator.SetBool("RoyalRun", false);
        animator.SetBool("RoyalIdle", false);

        // ��ƼŬ �ý��� ����
        if (deathParticles != null)
        {
            Instantiate(deathParticles, transform.position, Quaternion.identity);
        }

        Destroy(gameObject, 2f); // 2�� �� ������Ʈ ����
    }

    // �÷��̾�� �浹 �� ó���ϴ� �Լ�
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player playerScript = collision.gameObject.GetComponent<Player>();

            if (playerScript != null)
            {
                Debug.Log("Hit");
            }
        }
    }

    // ���� ���ظ� �޴� �Լ�
    public void TakeDamage(float amount)
    {
        health -= amount;

        // ���� �׾��� �� ó��
        if (health <= 0)
        {
            Die();
        }
    }
}
