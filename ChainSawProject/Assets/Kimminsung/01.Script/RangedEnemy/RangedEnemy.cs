using System.Collections;
using UnityEngine;

public class RangedEnemy : TestEnemy
{
    public float chaseSpeed = 2f; // �÷��̾� ���� �ӵ�
    public float chaseRange = 10f; // �÷��̾ ������ ����
    public float stopDistance = 2f; // ���� �Ÿ����� �÷��̾� ���� �� ����
    public float attackRange = 1.5f; // ���� �Ÿ�
    public float attackDamage = 10f; // ���� ������
    public float attackCooldown = 1f; // ���� ��ٿ� �ð�

    private Transform player; // �÷��̾��� ��ġ�� �����ϱ� ���� ����
    private Rigidbody2D rb; // Rigidbody2D ����
    private Animator animator; // �ִϸ����� ����
    private float lastAttackTime; // ������ ���� �ð�

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D ������Ʈ ��������
        player = GameObject.FindGameObjectWithTag("KPlayer").transform; // �÷��̾� ������Ʈ ã��
        animator = GetComponent<Animator>(); // Animator ������Ʈ ��������
        health = 25f; // RangedEnemy�� �ʱ� ü��
        maxHealth = health; // ü�� �����̴��� ���� �ִ� ü�� ����
    }

    private void Update()
    {
        // ���� �׾����� �� �̻� ������ �������� ����
        if (isDead) return;

        if (player != null && health > 0)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            // �÷��̾ ���� ���� ���� ���� ���� �̵�
            if (distanceToPlayer < chaseRange && distanceToPlayer > stopDistance)
            {
                ChasePlayer();
                animator.SetBool("BulletRun", true); // �̵� ���� �ִϸ��̼� ���
                animator.SetBool("BulletIdle", false); // ��� ���´� ��

                // ���� �Ÿ� ���� ���� ��� ���� �õ�
                if (distanceToPlayer <= attackRange && Time.time >= lastAttackTime + attackCooldown)
                {
                    AttackPlayer();
                }
            }
            else
            {
                rb.velocity = Vector2.zero; // ����
                animator.SetBool("BulletRun", false); // �̵� ���� �ִϸ��̼� ��
                animator.SetBool("BulletIdle", true); // ��� ���·� ��ȯ
            }
        }

        // ���� ������ ��� ó��
        if (health <= 0 && !isDead)
        {
            DieEffect(); // TestEnemy�� ��� ����Ʈ �Լ� ȣ��
        }
    }

    // �÷��̾ �����ϴ� �Լ�
    private void ChasePlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized; // �÷��̾� ������ ���� ���
        rb.velocity = direction * chaseSpeed; // ���� �ӵ���ŭ �̵�
    }

    // �÷��̾ �����ϴ� �Լ�
    private void AttackPlayer()
    {
        lastAttackTime = Time.time; // ������ ���� �ð� ������Ʈ
        Player playerScript = player.GetComponent<Player>(); // Player ��ũ��Ʈ ����

        if (playerScript != null)
        {
            playerScript.TakeHit(attackDamage, transform.position); // �÷��̾ ���� ���� ����
            animator.SetTrigger("Attack"); // ���� �ִϸ��̼� Ʈ����
        }
    }

    // ���� �޴� �Լ� (TestEnemy�� TakeHit() Ȱ��)
    public override void TakeHit(float damage, Vector2 hitPos)
    {
        if (isDead) return; // �̹� ���� ���� ������ ó�� �� ��

        // ü�� ���� ó��
        base.TakeHit(damage, hitPos);

        // ���� �׾��� �� ó��
        if (health <= 0 && !isDead)
        {
            DieEffect(); // ��� ����Ʈ ȣ��
        }
    }

    // DieEffect �������̵� (TestEnemy�� DieEffect() ȣ��)
    public override void DieEffect()
    {
        base.DieEffect(); // TestEnemy�� DieEffect() ����
        StartCoroutine(RemoveAfterDeath()); // 2�� �� ������Ʈ ����
    }

    // 2�� �� ������Ʈ ���� �ڷ�ƾ
    private IEnumerator RemoveAfterDeath()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject); // 2�� �� �� ����
    }
}
