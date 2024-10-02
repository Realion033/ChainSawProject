using System.Collections;
using UnityEngine;

public class shildEnemy : MonoBehaviour
{
    public float health = 25f; // ���ʹ� ü��
    public float shieldHealth = 200f; // ���� ü��
    public float damage = 10f; // ���� ������
    public float dashSpeed = 5f; // ���� �ӵ�
    public float dashDistance = 3f; // ���� �Ÿ�
    public float invulnerableTime = 3f; // ���� �ð�
    public float chaseSpeed = 2f; // �÷��̾� ���� �ӵ�
    public float chaseRange = 10f; // �÷��̾ ������ ����
    public float dashRange = 3f; // ���� ������ ������ ����

    private bool isDashing = false;
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

            // �÷��̾ ���� ���� ���� ���� ��
            if (distanceToPlayer < chaseRange && distanceToPlayer > dashRange)
            {
                ChasePlayer();
                animator.SetBool("SheildWalk", true); // �ȴ� �ִϸ��̼� ���
                animator.SetBool("SheildIdle", false); // ��� ���´� ��
            }
            // ���� ������ ����� ��
            else
            {
                rb.velocity = Vector2.zero; // ����
                animator.SetBool("SheildWalk", false); // �ȱ� �ִϸ��̼� ��
                animator.SetBool("SheildIdle", true); // ��� �ִϸ��̼� ���
            }

            // �÷��̾ ���� ���� ���� ���� ���� ��
            if (distanceToPlayer <= dashRange && !isDashing)
            {
                StartCoroutine(DashAttack());
            }

            // ���� ��� ���� ����
            if (Input.GetKeyDown(KeyCode.LeftShift)) // ���� ��� Ʈ����
            {
                StartCoroutine(ActivateInvulnerability());
            }

            // ���� �׾��� ��
            if (health <= 0)
            {
                Die();
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

    // ���� ���� ����
    IEnumerator DashAttack()
    {
        if (!isDashing)
        {
            isDashing = true;
            Debug.Log("���� ������"); // ���� �� ����� �޽���

            Vector2 dashDirection = (player.position - transform.position).normalized; // �÷��̾� ������ ����
            Vector2 targetPosition = (Vector2)transform.position + dashDirection * dashDistance; // ���� ��ǥ ��ġ ���

            // �����ϴ� ���� �ٷ� ��ǥ ��ġ�� �̵�
            rb.MovePosition(targetPosition);

            // ���� �� ����� ���ð� (2�� ���� ����)
            rb.velocity = Vector2.zero; // �ӵ� 0���� �����Ͽ� ���߰� ��
            yield return new WaitForSeconds(2f); // 2�� ���� ����

            isDashing = false; // ���� �Ϸ� �� �ٽ� ���� �����ϰ� ����
        }
    }

    // ���� ���� ���� Ȱ��ȭ
    IEnumerator ActivateInvulnerability()
    {
        if (!isInvulnerable)
        {
            isInvulnerable = true;
            yield return new WaitForSeconds(invulnerableTime);
            isInvulnerable = false;
        }
    }

    // ���� �״� �Լ�
    void Die()
    {
        animator.SetBool("SheildDie", true); // �״� �ִϸ��̼� ���
        rb.velocity = Vector2.zero; // ���߰� ��
        Destroy(gameObject, 2f); // 2�� �� ������Ʈ ����
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

            // ���� �׾��� ���
            if (health <= 0)
            {
                Die();
            }
        }
    }
}
