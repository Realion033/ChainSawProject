using UnityEngine;

namespace MIN
{
    public class KnifeDeadState : EnemyState<KnifeEnum>
    {
        private Animator _animator;
        private float _deathAnimationDuration = 1.0f; // �ִϸ��̼� ���� �ð� (��)

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

            // ���� �ִϸ��̼� Ʈ����
            _animator.SetTrigger("Die");

            // �ִϸ��̼��� ���� �� ���¸� �����ϴ� ���
            _enemyBase.Invoke(nameof(OnDeathAnimationComplete), _deathAnimationDuration);
        }

        public override void UpdateState()
        {
            // ���°� ������Ʈ�Ǵ� ���ȿ��� �ƹ� �۾��� ���� ����
        }

        private void OnDeathAnimationComplete()
        {
            // ���� �ִϸ��̼��� �Ϸ�� �� �ʿ� �� �߰� �۾� ����
            // ���� ���, ������Ʈ�� ��Ȱ��ȭ�ϰų� ����
            _enemyBase.gameObject.SetActive(false);
        }

        public override void Exit()
        {
            base.Exit();
            // ���� ���� �� �߰����� �۾��� �ʿ��� ��� ���⿡ �ۼ�
        }
    }
}
