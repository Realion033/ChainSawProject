using UnityEngine;

public class EnemyLookAtPlayer : MonoBehaviour
{
    public Transform player; // �÷��̾��� Transform
    private SpriteRenderer spriteRenderer; // ���ʹ��� SpriteRenderer

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // ���ʹ��� SpriteRenderer ������Ʈ�� ������
    }

    private void Update()
    {
        FlipTowardsPlayer();
    }

    // �÷��̾� ���⿡ ���� ���ʹ��� ������ �����ϴ� �Լ�
    private void FlipTowardsPlayer()
    {
        // �÷��̾��� ��ġ�� ���ʹ��� ��ġ ��
        if (player != null)
        {
            if (player.position.x < transform.position.x)
            {
                // �÷��̾ ���ʿ� ������ ���ʹ̰� ������ �ٶ󺸵��� flipX�� false�� ����
                spriteRenderer.flipX = true;
            }
            else
            {
                // �÷��̾ �����ʿ� ������ ���ʹ̰� �������� �ٶ󺸵��� flipX�� true�� ����
                spriteRenderer.flipX = false;
            }
        }
    }
}

