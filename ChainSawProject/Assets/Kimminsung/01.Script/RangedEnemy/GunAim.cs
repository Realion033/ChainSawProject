using UnityEngine;

public class GunAim : MonoBehaviour
{
    public Transform player; // 플레이어의 Transform
    public SpriteRenderer spriteRenderer; // 총구의 SpriteRenderer
    public float rotationSpeed = 5f; // 총구 회전 속도 (선택 사항)

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("KPlayer").transform;
    }
    private void Update()
    {
        if (player != null)
        {
            // 총구와 플레이어 사이의 벡터를 계산
            Vector2 direction = (player.position - transform.position).normalized;

            // 벡터의 각도를 계산
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // 총구를 플레이어 방향으로 회전
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(0, 0, angle)), rotationSpeed * Time.deltaTime);

            // 총구의 flipY를 조정하여 방향에 맞게 회전
            if (player.position.x < transform.position.x)
            {
                // 플레이어가 총구의 왼쪽에 있는 경우
                spriteRenderer.flipY = true;
            }
            else
            {
                // 플레이어가 총구의 오른쪽에 있는 경우
                spriteRenderer.flipY = false;
            }
        }
    }
}
