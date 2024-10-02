using UnityEngine;

public class KPLayer : MonoBehaviour
{
    public float moveSpeed = 5f; // �÷��̾� �̵� �ӵ�

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
    public void TakeDamage(int damage)
    {
        Debug.Log("���� ����~");
    }

    private void FixedUpdate()
    {
        // Rigidbody2D�� ����Ͽ� �÷��̾� �̵�
        move.velocity = movement * moveSpeed;
    }
}
