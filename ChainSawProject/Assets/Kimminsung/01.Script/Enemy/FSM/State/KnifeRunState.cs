using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MIN
{
    public class KnifeRunState : EnemyState<KnifeEnum>
    {
        public float moveSpeed = 5f; // 적의 이동 속도
        private SpriteRenderer spriteRenderer; // SpriteRenderer를 캐싱

        public KnifeRunState(Enemy enemyBase, EnemyStateMachine<KnifeEnum> stateMachine, string animBoolName)
            : base(enemyBase, stateMachine, animBoolName)
        {
            // SpriteRenderer 캐싱
            spriteRenderer = enemyBase.GetComponent<SpriteRenderer>();
            if (spriteRenderer == null)
            {
                Debug.LogError("SpriteRenderer를 찾을 수 없습니다!");
            }
        }

        public override void Enter()
        {
            base.Enter();

            // 스프라이트가 제대로 설정되었는지 확인하고 복구
            EnsureSpriteVisibility();
        }

        public override void UpdateState()
        {
            base.UpdateState();

            // 플레이어를 감지하는 로직
            Collider2D target = _enemyBase.IsPlayerDetected();
            if (target == null)
            {
                return; // 타겟이 없으면 실행 종료
            }

            Vector2 direction = target.transform.position - _enemyBase.transform.position;
            direction.y = 0; // 수직 이동 방지

            if (!_enemyBase.IsObstacleInLine(direction.magnitude, direction.normalized))
            {
                _enemyBase.targetTrm = target.transform;

                Vector2 moveDirection = direction.normalized * moveSpeed * Time.deltaTime;
                _enemyBase.transform.Translate(moveDirection, Space.World);

                // 타겟과의 거리가 일정 범위 내에 들어오면 공격 상태로 전환
                if (Vector3.Distance(_enemyBase.transform.position, target.transform.position) <= _enemyBase.attackDistance)
                {
                    _stateMachine.ChangeState(KnifeEnum.Attack);
                }
            }

            // 스프라이트 상태가 제대로 유지되는지 확인
            EnsureSpriteVisibility();
        }

        public override void Exit()
        {
            base.Exit();
            // 상태 종료 시 필요한 처리를 여기에 작성
        }

        // 스프라이트의 상태를 점검하고 복구하는 메서드
        private void EnsureSpriteVisibility()
        {
            if (spriteRenderer == null) return;

            // 1. SpriteRenderer가 활성화 상태인지 확인
            if (!spriteRenderer.enabled)
            {
                Debug.LogWarning("SpriteRenderer가 비활성화 상태입니다. 다시 활성화합니다.");
                spriteRenderer.enabled = true;
            }

            // 2. 알파 값이 0인지 확인
            if (spriteRenderer.color.a == 0)
            {
                Debug.LogWarning("스프라이트의 알파 값이 0입니다. 알파 값을 복구합니다.");
                Color newColor = spriteRenderer.color;
                newColor.a = 1f; // 알파 값을 1로 설정하여 불투명하게 만듦
                spriteRenderer.color = newColor;
            }

            // 3. 스프라이트가 올바르게 설정되었는지 확인
            if (spriteRenderer.sprite == null)
            {
                Debug.LogError("SpriteRenderer에 스프라이트가 설정되지 않았습니다!");
            }

            // 4. 스프라이트가 다른 오브젝트에 의해 가려지지 않도록 레이어 설정 확인
            if (spriteRenderer.sortingOrder <= 0) // 예를 들어 기본값이 0일 때
            {
                Debug.LogWarning("스프라이트의 레이어 순서가 낮습니다. 다시 설정합니다.");
                spriteRenderer.sortingOrder = 1; // 필요에 따라 값을 변경
            }
        }
    }
}
