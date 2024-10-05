using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 10f; // �Ѿ� ������ ����

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("KPlayer"))
        {
            Player player = other.GetComponent<Player>(); // Player ��ũ��Ʈ ����

            if (player != null)
            {
                player.TakeHit(damage, transform.position); // �÷��̾�� ���� ����
            }

            // �Ѿ� ����
            Destroy(gameObject);
        }
    }
}
