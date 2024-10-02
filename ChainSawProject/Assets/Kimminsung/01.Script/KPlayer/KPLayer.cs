using UnityEngine;

public class KPLayer : MonoBehaviour
{
    public float moveSpeed = 5f; // 플레이어 이동 속도

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
    public void TakeDamage(int damage)
    {
        Debug.Log("엄마 아파~");
    }

    private void FixedUpdate()
    {
        // Rigidbody2D를 사용하여 플레이어 이동
        move.velocity = movement * moveSpeed;
    }
}
