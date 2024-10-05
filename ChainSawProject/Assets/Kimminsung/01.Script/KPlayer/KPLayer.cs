using UnityEngine;

public class KPLayer : MonoBehaviour
{
    public float moveSpeed = 5f; // �÷��̾� �̵� �ӵ�
    public float health = 100f; // �÷��̾� ü��

    private Rigidbody2D move;
    private Vector2 movement;

    private void Start()
    {
        move = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // �Է°��� ��������
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        movement = new Vector2(moveX, moveY).normalized;
    }

    // ���ظ� �޴� �޼���
    public void TakeDamage(float damage)
    {
        health -= damage; // ü�� ����
        Debug.Log($"���� ����~ ü��: {health}");

        if (health <= 0)
        {
            Die(); // ü���� 0 ������ �� ���� ó��
        }
    }

    // �÷��̾ �׾��� �� ó��
    private void Die()
    {
        Debug.Log("�÷��̾ �׾����ϴ�!");
        // �߰����� ���� ó�� ���� (��: ���� ���� ȭ�� ǥ�� ��)
        // ���� ���, �÷��̾� ������Ʈ ��Ȱ��ȭ
        gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        // Rigidbody2D�� ����Ͽ� �÷��̾� �̵�
        move.velocity = movement * moveSpeed;
    }
}
