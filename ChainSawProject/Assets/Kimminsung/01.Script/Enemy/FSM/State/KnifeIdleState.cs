using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MIN
{
    public class KnifeIdleState : EnemyState<KnifeEnum>
    {
        public KnifeIdleState(Enemy enemyBase, EnemyStateMachine<KnifeEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
        {
        }

        public override void UpdateState()
        {
            base.UpdateState();
            Collider2D target = _enemyBase.IsPlayerDetected();
            if (target == null) return; //�ֺ��� �÷��̾ ������ �ƹ��͵� ����.

            Vector3 direction = target.transform.position - _enemyBase.transform.position;
            direction.y = 0;

            //�÷��̾� �߰��߰� �� ���̿� ��ֹ��� ����.
            if (_enemyBase.IsObstacleInLine(direction.magnitude, direction.normalized) == false)
            {
                _enemyBase.targetTrm = target.transform;
                _stateMachine.ChangeState(KnifeEnum.Attack);//�������·� ��ȯ
            }
        }
    }
}
