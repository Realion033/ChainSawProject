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

        // ������
        public KnifeAttackState(Enemy enemyBase, EnemyStateMachine<KnifeEnum> stateMachine, string animBoolName)
            : base(enemyBase, stateMachine, animBoolName)
        {
            animator = enemyBase.GetComponent<Animator>();

            if (animator == null)
            {
                Debug.LogWarning("Animator not found on the KnifeEnemy.");
            }
        }

        // ���� ���� �� ȣ��
        public override void Enter()
        {
            base.Enter();
            isAttacking = true;
            Debug.Log("Entering Attack State");
        }

        // ���� ������Ʈ
        public override void UpdateState()
        {
            base.UpdateState();
            if (isAttacking && _enemyBase.targetTrm != null)
            {
                // Ÿ���� ���� ���� ���� �ִ��� Ȯ��
                if (Vector3.Distance(_enemyBase.transform.position, _enemyBase.targetTrm.position) <= attackRange)
                {
                    PerformAttack();
                }
            }
        }

        // ���� ���� �� ȣ��
        public override void Exit()
        {
            base.Exit();
            isAttacking = false;
            Debug.Log("Exiting Attack State");
        }

        // ���� ����
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



