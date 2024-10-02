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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D ������Ʈ ��������
        player = GameObject.FindGameObjectWithTag("Player").transform; // �÷��̾� ������Ʈ ã��
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
            }

            // �÷��̾ ���� ���� ���� ���� ���� ��
            else if (distanceToPlayer <= dashRange)
            {
                StartCoroutine(DashAttack());
            }

            // ���� ��� ���� ����
            if (Input.GetKeyDown(KeyCode.LeftShift)) // ���� ��� Ʈ����
            {
                StartCoroutine(ActivateInvulnerability());
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
        }
    }
}
