using UnityEngine;

public class GunAim : MonoBehaviour
{
    public Transform player; // �÷��̾��� Transform
    public SpriteRenderer spriteRenderer; // �ѱ��� SpriteRenderer
    public float rotationSpeed = 5f; // �ѱ� ȸ�� �ӵ� (���� ����)

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("KPlayer").transform;
    }
    private void Update()
    {
        if (player != null)
        {
            // �ѱ��� �÷��̾� ������ ���͸� ���
            Vector2 direction = (player.position - transform.position).normalized;

            // ������ ������ ���
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // �ѱ��� �÷��̾� �������� ȸ��
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(0, 0, angle)), rotationSpeed * Time.deltaTime);

            // �ѱ��� flipY�� �����Ͽ� ���⿡ �°� ȸ��
            if (player.position.x < transform.position.x)
            {
                // �÷��̾ �ѱ��� ���ʿ� �ִ� ���
                spriteRenderer.flipY = true;
            }
            else
            {
                // �÷��̾ �ѱ��� �����ʿ� �ִ� ���
                spriteRenderer.flipY = false;
            }
        }
    }
}
