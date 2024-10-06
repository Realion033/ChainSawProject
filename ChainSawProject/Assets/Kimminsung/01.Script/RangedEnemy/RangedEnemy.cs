using System;
using System.Collections;
using UnityEngine;

public class RangedEnemy : TestEnemy
{
    public float chaseSpeed = 2f; // �÷��̾� ���� �ӵ�
    public float chaseRange = 10f; // �÷��̾ ������ ����
    public float stopDistance = 2f; // ���� �Ÿ����� �÷��̾� ���� �� ����

    private Transform player; // �÷��̾��� ��ġ�� �����ϱ� ���� ����
    private Rigidbody2D rb; // Rigidbody2D ����
    private Animator animator; // �ִϸ����� ����

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
        // �÷��̾��� ��ġ���� X ��ǥ�� ����ϰ�, Y�� ���� ���� Y ��ġ�� ����
        Vector2 direction = new Vector2(player.position.x - transform.position.x, 0).normalized;

        // ���� �ӵ���ŭ �̵� (Y���� 0���� ������)
        rb.velocity = new Vector2(direction.x * chaseSpeed, rb.velocity.y);
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
        StartCoroutine(Hitstop());
        player.GetComponent<LivingEntity>().health = player.GetComponent<Player>()._playerStat.playerHealth;
    }

    private IEnumerator Hitstop()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(0.16f);
        Time.timeScale = 1;
    }

    // 2�� �� ������Ʈ ���� �ڷ�ƾ
    private IEnumerator RemoveAfterDeath()
    {
        yield return new WaitForSeconds(0.1f);
        // �θ� ������Ʈ�� �����Ͽ� �ڽ� ������Ʈ�� �Բ� ����
        Destroy(gameObject); // 2�� �� �� ����
    }

}
