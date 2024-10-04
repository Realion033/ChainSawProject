using System.Collections;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    public float health = 25f; // ���ʹ� ü��
    public float shieldHealth = 200f; // ���� ü��
    public float damage = 10f; // ���� ������
    public float chaseSpeed = 2f; // �÷��̾� ���� �ӵ�
    public float chaseRange = 10f; // �÷��̾ ������ ����
    public float stopDistance = 2f; // ���� �Ÿ����� �÷��̾� ���� �� ����

    private bool isInvulnerable = false;

    private Transform player; // �÷��̾��� ��ġ�� �����ϱ� ���� ����
    private Rigidbody2D rb; // Rigidbody2D ����
    private Animator animator; // �ִϸ����� ����

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D ������Ʈ ��������
        player = GameObject.FindGameObjectWithTag("KPlayer").transform; // �÷��̾� ������Ʈ ã��
        animator = GetComponent<Animator>(); // Animator ������Ʈ ��������
    }

    void Update()
    {
        if (health > 0 && player != null)
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
                rb.velocity = Vector2.zero; // ���� �Ÿ� �̻� ���� �� ����
                animator.SetBool("BulletRun", false); // �̵� ���� �ִϸ��̼� ��
                animator.SetBool("BulletIdle", true); // ��� ���·� ��ȯ
            }

            // �÷��̾� ��� ó��
            if (health <= 0)
            {
                animator.SetBool("BulletDie", true); // ��� �ִϸ��̼� ���
                Destroy(gameObject, 2f); // 2�� �� ������Ʈ ����
            }
        }
        else
        {
            Destroy(gameObject); // ���� ������ ������Ʈ ����
        }
    }

    // �÷��̾ �����ϴ� �Լ�
    void ChasePlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized; // �÷��̾� ������ ���� ���
        rb.velocity = direction * chaseSpeed; // ���� �ӵ���ŭ �̵�
    }

    // ���� ���� ���� Ȱ��ȭ
    IEnumerator ActivateInvulnerability()
    {
        if (!isInvulnerable)
        {
            isInvulnerable = true;
            yield return new WaitForSeconds(3f); // ���� �ð�
            isInvulnerable = false;
        }
    }

    // ���� �޴� �Լ�
    public void TakeDamage(float amount)
    {
        if (!isInvulnerable)
        {
            if (shieldHealth > 0)
            {
                shieldHealth -= amount; // ���� ü�� �Ҹ�
            }
            else
            {
                health -= amount; // ���� ü���� 0�̸� ���� ü�� �Ҹ�
            }

            // ���� �׾��� ��� ��� �ִϸ��̼� ���
            if (health <= 0)
            {
                animator.SetBool("BulletDie", true); // ��� �ִϸ��̼� ���
                Destroy(gameObject, 2f); // 2�� �� ������Ʈ ����
            }
        }
    }
}
