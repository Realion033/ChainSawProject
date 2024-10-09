using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class RangedEnemy : LivingEntity
{
    private Collider2D col;
    public GameObject gun;
    public GameObject gdd;
    public AudioClip _hit;
    private AudioSource _audio;
    [SerializeField] GameObject f;
    [SerializeField] protected ParticleSystem _blood;
    public float chaseSpeed = 2f; // �÷��̾� ���� �ӵ�
    public float chaseRange = 10f; // �÷��̾ ������ ����
    public float stopDistance = 2f; // ���� �Ÿ����� �÷��̾� ���� �� ����

    private SpriteRenderer _spriteRenderer;
    private Slider _slider;
    private float maxHealth;
    private Transform player; // �÷��̾��� ��ġ�� �����ϱ� ���� ����
    private Rigidbody2D rb; // Rigidbody2D ����
    private Animator animator; // �ִϸ����� ����

    private int cnt = 0;

    private void Start()
    {
        col = GetComponent<Collider2D>();
        _audio = GetComponent<AudioSource>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D ������Ʈ ��������
        player = GameObject.FindGameObjectWithTag("KPlayer").transform; // �÷��̾� ������Ʈ ã��
        animator = GetComponent<Animator>(); // Animator ������Ʈ ��������
        _slider = GetComponentInChildren<Slider>();

        maxHealth = health;
    }

    public override void TakeHit(float damage, Vector2 hitPos)
    {
        _audio.PlayOneShot(_hit);
        base.TakeHit(damage, hitPos);
        if (!isDead)
        {
            Instantiate(_blood, hitPos, Quaternion.identity);
            StartCoroutine(FlashRed());
        }
        if (isDead && cnt == 0)
        {
            Instantiate(_blood, hitPos, Quaternion.identity);
            cnt++;
        }
    }
    private void Update()
    {
        ui();

        // Do nothing if dead
        if (isDead) return;

        if (player != null && health > 0)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            // If player is beyond the stop distance, chase them
            if (distanceToPlayer > stopDistance)
            {
                ChasePlayer();
                animator.SetBool("BulletRun", true); // Set run animation
                animator.SetBool("BulletIdle", false); // Disable idle animation
            }
            else
            {
                rb.velocity = Vector2.zero; // Stop moving
                animator.SetBool("BulletRun", false); // Disable run animation
                animator.SetBool("BulletIdle", true); // Enable idle animation
            }
        }

        // Handle death
        if (health <= 0 && !isDead)
        {
            DieEffect(); // Trigger death effect
        }
    }

    private void ui()
    {
        _slider.value = health / maxHealth;

        if (isDead)
        {
            f.SetActive(false);
        }
        else
        {
            f.SetActive(true);
        }
    }

    // �÷��̾ �����ϴ� �Լ�
    private void ChasePlayer()
    {
        // �÷��̾��� ��ġ���� X ��ǥ�� ����ϰ�, Y�� ���� ���� Y ��ġ�� ����
        Vector2 direction = new Vector2(player.position.x - transform.position.x, 0).normalized;

        // ���� �ӵ���ŭ �̵� (Y���� 0���� ������)
        rb.velocity = new Vector2(direction.x * chaseSpeed, rb.velocity.y);
    }

    // ���� �޴� �Լ� (TestEnemy�� TakeHit() Ȱ��)

    // DieEffect �������̵� (TestEnemy�� DieEffect() ȣ��)
    public override void DieEffect()
    {
        base.DieEffect(); // TestEnemy�� DieEffect() ����
        StartCoroutine(Dieef());
        gun.SetActive(false);
        col.isTrigger = true;
        gdd.SetActive(false);
        StartCoroutine(RemoveAfterDeath()); // 2�� �� ������Ʈ ����
        player.GetComponent<LivingEntity>().health += UnityEngine.Random.Range(3, 5);
    }


    // 2�� �� ������Ʈ ���� �ڷ�ƾ
    private IEnumerator RemoveAfterDeath()
    {
        yield return new WaitForSeconds(0.8f);
        // �θ� ������Ʈ�� �����Ͽ� �ڽ� ������Ʈ�� �Բ� ����
        Destroy(gameObject); // 2�� �� �� ����
    }
    private IEnumerator Dieef()
    {
        // 처음에 빨간색으로 변환
        _spriteRenderer.color = new Color(1f, 0f, 0f, 1f); // 빨간색, 알파값 1 (불투명)

        float fadeDuration = 0.2f; // 페이드 아웃이 걸리는 시간 (초)
        float fadeSpeed = 1f / fadeDuration; // 페이드 아웃 속도

        for (float t = 0; t < 1; t += Time.deltaTime * fadeSpeed)
        {
            Color newColor = _spriteRenderer.color;
            newColor.a = Mathf.Lerp(1f, 0f, t); // 알파값을 1에서 0으로 서서히 줄임
            _spriteRenderer.color = newColor;
            yield return null;
        }
        // 완전히 투명해지면 오브젝트를 비활성화하거나 제거
        _spriteRenderer.color = new Color(1f, 0f, 0f, 0f); // 완전히 투명한 상태
    }

    private IEnumerator FlashRed()
    {
        _spriteRenderer.color = Color.red;

        float duration = 0.1f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            _spriteRenderer.color = Color.Lerp(Color.red, Color.white, elapsed / duration);
            yield return null;
        }

        _spriteRenderer.color = Color.white;
    }

}
