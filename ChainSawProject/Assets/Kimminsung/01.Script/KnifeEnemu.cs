using System.Collections;
using UnityEngine;

public class KnifeEnemy : LivingEntity
{
    public float jumpHeight = 5f; // ���� ����
    public float fallSpeed = 10f; // ���� �ӵ�
    public float attackCooldown = 7f; // ���� ��Ÿ��
    public float attackRange = 5f; // ���� ����
    public float chaseSpeed = 3f; // ���� �ӵ�
    public ParticleSystem deathParticles; // ���� �� ����� ��ƼŬ �ý���
    public float damage = 10f;
    public float maxHealth;

    private Animator animator;
    private Transform player; // �÷��̾��� ��ġ�� �����ϱ� ���� ����
    private Rigidbody2D rb; // Rigidbody2D ����
    private float nextAttackTime = 0f; // ���� ���ݱ��� ���� �ð�
    private bool isAttacking = false; // ���� ������ ���θ� ��Ÿ���� ����

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D ������Ʈ ��������
        player = GameObject.FindGameObjectWithTag("KPlayer").transform; // �÷��̾� ������Ʈ ã��
        health = 100f; // RoyalEnemy�� �ʱ� ü��
        maxHealth = health; // �ִ� ü�� ����
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isDead) return; // ���� ������ �ƹ��͵� �� ��

        if (health <= 0)
        {
            Die();
            return; // �׾��� ���� �� �̻� �������� ����
        }

        float distanceToPlayer = Mathf.Abs(player.position.x - transform.position.x); // X�� �Ÿ� ���

        // �÷��̾��� ��ġ���� X ��ǥ�� ����ϰ�, Y�� ���� ���� Y ��ġ�� ����
        Vector2 direction = new Vector2(player.position.x - transform.position.x, 0).normalized;

        // ���� ���� �ƴ� ���� ����
        if (!isAttacking)
        {
            animator.SetTrigger("Run"); // �޸��� �ִϸ��̼� Ʈ����
            rb.velocity = new Vector2(direction.x * chaseSpeed, rb.velocity.y); // ���� �ӵ���ŭ �̵�
        }

        if (distanceToPlayer <= attackRange && Time.time >= nextAttackTime)
        {
            StartCoroutine(PerformJumpAttack());
        }
    }

    // ���� ���� ����
    private IEnumerator PerformJumpAttack()
    {
        isAttacking = true; // ���� ���·� ����
        animator.SetTrigger("Idle"); // Idle �ִϸ��̼� Ʈ����
        animator.SetTrigger("Attack"); // ���� �ִϸ��̼� Ʈ����

        // ���� ������ X�����θ� �ϵ��� ����
        Vector2 attackDirection = new Vector2(player.position.x - transform.position.x, 0).normalized;
        rb.velocity = new Vector2(attackDirection.x, 0) * fallSpeed; // X�����θ� ����

        yield return new WaitForSeconds(0.3f); // ���� �� ���

        rb.velocity = Vector2.zero; // �ӵ� 0���� �����Ͽ� ���� �� ����
        nextAttackTime = Time.time + attackCooldown; // ���� ��Ÿ�� ����

        isAttacking = false; // ���� ���� ����
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.collider.CompareTag("KPlayer") && isAttacking) // ���� ���� ���� ������ ����
        {
            Player player = other.collider.GetComponent<Player>(); // Player ��ũ��Ʈ ����

            if (player != null)
            {
                player.TakeHit(damage, transform.position); // �÷��̾�� ���� ����
            }
        }
    }

    // ���� �׾��� �� ȣ��Ǵ� �Լ�
    public override void DieEffect()
    {
        base.DieEffect(); // TestEnemy�� DieEffect() ȣ��

        // ��ƼŬ �ý��� ����
        if (deathParticles != null)
        {
            Instantiate(deathParticles, transform.position, Quaternion.identity);
        }

        // �ڷ�ƾ ����: Time.timeScale 0 -> ��� -> Time.timeScale 1 ����
        //StartCoroutine(DieAndPause());
    }

    // �ڷ�ƾ: ���� �� Ÿ�ӽ����� ���� �� ���� �ð� ���
    private IEnumerator DieAndPause()
    {
        Time.timeScale = 0; // ���� �Ͻ�����
        yield return new WaitForSecondsRealtime(0.1f); // ���� �ð����� 0.1�� ���
        Time.timeScale = 1; // �ٽ� ���� �簳
        Destroy(gameObject); // �� ������Ʈ ����
    }

    // ���� ���ظ� �޴� �Լ�
    public override void TakeHit(float damage, Vector2 hitPos)
    {
        base.TakeHit(damage, hitPos);

        // ���� �׾��� �� ó��
        if (health <= 0)
        {
            DieEffect(); // ��� ����Ʈ ȣ��
        }
    }
}
