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

    private Animator animator;
    private Transform player; // �÷��̾��� ��ġ�� �����ϱ� ���� ����
    private Rigidbody2D rb; // Rigidbody2D ����
    private float nextAttackTime = 0f; // ���� ���ݱ��� ���� �ð�

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D ������Ʈ ��������
        player = GameObject.FindGameObjectWithTag("KPlayer").transform; // �÷��̾� ������Ʈ ã��
        health = 100f; // RoyalEnemy�� �ʱ� ü��
        maxHealth = health; // �ִ� ü�� ����
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isDead) return; // ���� ������ �ƹ��͵� �� ��

        if (health <= 0)
        {
            Die();
            return; // �׾��� ���� �� �̻� �������� ����
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // �÷��̾ �����ϴ� ���� ��
        if (distanceToPlayer > attackRange)
        {
            ChasePlayer();
        }
        else if (distanceToPlayer <= attackRange && Time.time >= nextAttackTime)
        {
            StartCoroutine(PerformJumpAttack());
        }
    }

    // �÷��̾ �����ϴ� �Լ�
    private void ChasePlayer()
    {
        if(player.position.x > transform.position.x)
            rb.velocity = new Vector2(chaseSpeed, rb.velocity.y);
        
        if(player.position.x < transform.position.x)
            rb.velocity = new Vector2(-chaseSpeed, rb.velocity.y);
        
        animator.SetBool("RoyalRun", true); // RoyalRun �ִϸ��̼� ����
        animator.SetBool("RoyalIdle", false); // RoyalIdle ��Ȱ��ȭ
    }

    // ���� ���� ����
    private IEnumerator PerformJumpAttack()
    {
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
    }

    // ���� �׾��� �� ȣ��Ǵ� �Լ�
    public override void DieEffect()
    {
        base.DieEffect(); // TestEnemy�� DieEffect() ȣ��

        // ��ƼŬ �ý��� ����
        if (deathParticles != null)
        {
            Instantiate(deathParticles, transform.position, Quaternion.identity);
        }
    }

    // ���� ���ظ� �޴� �Լ�
    public override void TakeHit(float damage, Vector2 hitPos)
    {
        base.TakeHit(damage, hitPos);

        // ���� �׾��� �� ó��
        if (health <= 0)
        {
            DieEffect(); // ��� ����Ʈ ȣ��
        }
    }
}
