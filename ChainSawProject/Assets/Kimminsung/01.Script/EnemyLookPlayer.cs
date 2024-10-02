using UnityEngine;

public class EnemyLookAtPlayer : MonoBehaviour
{
    public Transform player; // �÷��̾��� Transform�� �����մϴ�.
    private SpriteRenderer spriteRenderer; // ��������Ʈ �������� �����մϴ�.

    private void Start()
    {
        // ��������Ʈ �������� �ʱ�ȭ�մϴ�.
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // ���� �÷��̾��� ������ ����մϴ�.
        Vector2 direction = player.position - transform.position;

        // �÷��̾ ���� �����ʿ� ������ FlipX�� false�� �����մϴ�.
        // �÷��̾ ���� ���ʿ� ������ FlipX�� true�� �����մϴ�.
        if (direction.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }
}
