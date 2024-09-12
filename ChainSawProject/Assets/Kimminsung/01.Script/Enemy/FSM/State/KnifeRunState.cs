using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MIN
{
    public class KnifeRunState : EnemyState<KnifeEnum>
    {
        public float moveSpeed = 5f; // ���� �̵� �ӵ�
        private SpriteRenderer spriteRenderer; // SpriteRenderer�� ĳ��

        public KnifeRunState(Enemy enemyBase, EnemyStateMachine<KnifeEnum> stateMachine, string animBoolName)
            : base(enemyBase, stateMachine, animBoolName)
        {
            // SpriteRenderer ĳ��
            spriteRenderer = enemyBase.GetComponent<SpriteRenderer>();
            if (spriteRenderer == null)
            {
                Debug.LogError("SpriteRenderer�� ã�� �� �����ϴ�!");
            }
        }

        public override void Enter()
        {
            base.Enter();

            // ��������Ʈ�� ����� �����Ǿ����� Ȯ���ϰ� ����
            EnsureSpriteVisibility();
        }

        public override void UpdateState()
        {
            base.UpdateState();

            // �÷��̾ �����ϴ� ����
            Collider2D target = _enemyBase.IsPlayerDetected();
            if (target == null)
            {
                return; // Ÿ���� ������ ���� ����
            }

            Vector2 direction = target.transform.position - _enemyBase.transform.position;
            direction.y = 0; // ���� �̵� ����

            if (!_enemyBase.IsObstacleInLine(direction.magnitude, direction.normalized))
            {
                _enemyBase.targetTrm = target.transform;

                Vector2 moveDirection = direction.normalized * moveSpeed * Time.deltaTime;
                _enemyBase.transform.Translate(moveDirection, Space.World);

                // Ÿ�ٰ��� �Ÿ��� ���� ���� ���� ������ ���� ���·� ��ȯ
                if (Vector3.Distance(_enemyBase.transform.position, target.transform.position) <= _enemyBase.attackDistance)
                {
                    _stateMachine.ChangeState(KnifeEnum.Attack);
                }
            }

            // ��������Ʈ ���°� ����� �����Ǵ��� Ȯ��
            EnsureSpriteVisibility();
        }

        public override void Exit()
        {
            base.Exit();
            // ���� ���� �� �ʿ��� ó���� ���⿡ �ۼ�
        }

        // ��������Ʈ�� ���¸� �����ϰ� �����ϴ� �޼���
        private void EnsureSpriteVisibility()
        {
            if (spriteRenderer == null) return;

            // 1. SpriteRenderer�� Ȱ��ȭ �������� Ȯ��
            if (!spriteRenderer.enabled)
            {
                Debug.LogWarning("SpriteRenderer�� ��Ȱ��ȭ �����Դϴ�. �ٽ� Ȱ��ȭ�մϴ�.");
                spriteRenderer.enabled = true;
            }

            // 2. ���� ���� 0���� Ȯ��
            if (spriteRenderer.color.a == 0)
            {
                Debug.LogWarning("��������Ʈ�� ���� ���� 0�Դϴ�. ���� ���� �����մϴ�.");
                Color newColor = spriteRenderer.color;
                newColor.a = 1f; // ���� ���� 1�� �����Ͽ� �������ϰ� ����
                spriteRenderer.color = newColor;
            }

            // 3. ��������Ʈ�� �ùٸ��� �����Ǿ����� Ȯ��
            if (spriteRenderer.sprite == null)
            {
                Debug.LogError("SpriteRenderer�� ��������Ʈ�� �������� �ʾҽ��ϴ�!");
            }

            // 4. ��������Ʈ�� �ٸ� ������Ʈ�� ���� �������� �ʵ��� ���̾� ���� Ȯ��
            if (spriteRenderer.sortingOrder <= 0) // ���� ��� �⺻���� 0�� ��
            {
                Debug.LogWarning("��������Ʈ�� ���̾� ������ �����ϴ�. �ٽ� �����մϴ�.");
                spriteRenderer.sortingOrder = 1; // �ʿ信 ���� ���� ����
            }
        }
    }
}
