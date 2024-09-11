using MIN;
using UnityEngine;

namespace MIN 
{
    public class KnifeAttackState : EnemyState<KnifeEnum>
    {
        public int damage = 10;
        public float attackSpeed = 1.5f;
        public float attackRange = 2.0f;

        private bool isAttacking = false;
        private Transform target;
        private Animator animator;

        // 생성자
        public KnifeAttackState(Enemy enemyBase, EnemyStateMachine<KnifeEnum> stateMachine, string animBoolName)
            : base(enemyBase, stateMachine, animBoolName)
        {
            animator = enemyBase.GetComponent<Animator>();

            if (animator == null)
            {
                Debug.LogWarning("Animator not found on the KnifeEnemy.");
            }
        }

        // 상태 진입 시 호출
        public override void Enter()
        {
            base.Enter();
            isAttacking = true;
            Debug.Log("Entering Attack State");
        }

        // 상태 업데이트
        public override void UpdateState()
        {
            base.UpdateState();
            if (isAttacking && _enemyBase.targetTrm != null)
            {
                // 타겟이 공격 범위 내에 있는지 확인
                if (Vector3.Distance(_enemyBase.transform.position, _enemyBase.targetTrm.position) <= attackRange)
                {
                    PerformAttack();
                }
            }
        }

        // 상태 종료 시 호출
        public override void Exit()
        {
            base.Exit();
            isAttacking = false;
            Debug.Log("Exiting Attack State");
        }

        // 공격 수행
        public void PerformAttack()
        {
            if (animator != null)
            {
                animator.SetTrigger("Attack");
            }

            Debug.Log("KnifeEnemy attacks with damage: " + damage);
        }
    }
}



