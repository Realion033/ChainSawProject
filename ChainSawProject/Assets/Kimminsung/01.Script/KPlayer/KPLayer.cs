using UnityEngine;

public class KPLayer : MonoBehaviour
{
    public float moveSpeed = 5f; // 플레이어 이동 속도
    public float health = 100f; // 플레이어 체력

    private Rigidbody2D move;
    private Vector2 movement;

    private void Start()
    {
        move = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // 입력값을 가져오기
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        movement = new Vector2(moveX, moveY).normalized;
    }

    // 피해를 받는 메서드
    public void TakeDamage(float damage)
    {
        health -= damage; // 체력 감소
        Debug.Log($"엄마 아파~ 체력: {health}");

        if (health <= 0)
        {
            Die(); // 체력이 0 이하일 때 죽음 처리
        }
    }

    // 플레이어가 죽었을 때 처리
    private void Die()
    {
        Debug.Log("플레이어가 죽었습니다!");
        // 추가적인 죽음 처리 로직 (예: 게임 오버 화면 표시 등)
        // 예를 들어, 플레이어 오브젝트 비활성화
        gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        // Rigidbody2D를 사용하여 플레이어 이동
        move.velocity = movement * moveSpeed;
    }
}
