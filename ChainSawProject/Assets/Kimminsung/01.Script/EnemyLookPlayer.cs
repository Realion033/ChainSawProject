using UnityEngine;

public class EnemyLookAtPlayer : MonoBehaviour
{
    public Transform player; // 플레이어의 Transform을 참조합니다.
    private SpriteRenderer spriteRenderer; // 스프라이트 렌더러를 참조합니다.

    private void Start()
    {
        // 스프라이트 렌더러를 초기화합니다.
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // 적과 플레이어의 방향을 계산합니다.
        Vector2 direction = player.position - transform.position;

        // 플레이어가 적의 오른쪽에 있으면 FlipX를 false로 설정합니다.
        // 플레이어가 적의 왼쪽에 있으면 FlipX를 true로 설정합니다.
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
