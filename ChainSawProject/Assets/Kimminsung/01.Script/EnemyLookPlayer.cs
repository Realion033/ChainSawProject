using UnityEngine;

public class EnemyLookAtPlayer : MonoBehaviour
{
    public Transform player; // 플레이어의 Transform
    private SpriteRenderer spriteRenderer; // 에너미의 SpriteRenderer

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // 에너미의 SpriteRenderer 컴포넌트를 가져옴
    }

    private void Update()
    {
        FlipTowardsPlayer();
    }

    // 플레이어 방향에 따라 에너미의 방향을 변경하는 함수
    private void FlipTowardsPlayer()
    {
        // 플레이어의 위치와 에너미의 위치 비교
        if (player != null)
        {
            if (player.position.x < transform.position.x)
            {
                // 플레이어가 왼쪽에 있으면 에너미가 왼쪽을 바라보도록 flipX를 false로 설정
                spriteRenderer.flipX = true;
            }
            else
            {
                // 플레이어가 오른쪽에 있으면 에너미가 오른쪽을 바라보도록 flipX를 true로 설정
                spriteRenderer.flipX = false;
            }
        }
    }
}

