using UnityEngine;

namespace MIN
{
    public class KnifeDeadState : EnemyState<KnifeEnum>
    {
        private Animator _animator;
        private float _deathAnimationDuration = 1.0f; // 애니메이션 지속 시간 (초)

        public KnifeDeadState(Enemy enemyBase, EnemyStateMachine<KnifeEnum> stateMachine, string animBoolName)
            : base(enemyBase, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _animator = _enemyBase.GetComponent<Animator>();

            if (_animator == null)
            {
                Debug.LogWarning("Animator not found on KnifeEnemy.");
                return;
            }

            // 죽음 애니메이션 트리거
            _animator.SetTrigger("Die");

            // 애니메이션이 끝난 후 상태를 종료하는 방법
            _enemyBase.Invoke(nameof(OnDeathAnimationComplete), _deathAnimationDuration);
        }

        public override void UpdateState()
        {
            // 상태가 업데이트되는 동안에는 아무 작업도 하지 않음
        }

        private void OnDeathAnimationComplete()
        {
            // 죽음 애니메이션이 완료된 후 필요 시 추가 작업 수행
            // 예를 들어, 오브젝트를 비활성화하거나 제거
            _enemyBase.gameObject.SetActive(false);
        }

        public override void Exit()
        {
            base.Exit();
            // 상태 종료 시 추가적인 작업이 필요한 경우 여기에 작성
        }
    }
}
