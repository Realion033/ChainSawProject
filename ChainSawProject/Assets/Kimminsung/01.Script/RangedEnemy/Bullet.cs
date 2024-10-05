using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 10f; // 총알 데미지 설정

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("KPlayer"))
        {
            Player player = other.GetComponent<Player>(); // Player 스크립트 참조

            if (player != null)
            {
                player.TakeHit(damage, transform.position); // 플레이어에게 피해 전달
            }

            // 총알 삭제
            Destroy(gameObject);
        }
    }
}
