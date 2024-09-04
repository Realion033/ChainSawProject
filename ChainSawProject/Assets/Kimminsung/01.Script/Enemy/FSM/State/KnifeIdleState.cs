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
            if (target == null) return; //주변에 플레이어가 없으면 아무것도 안함.

            Vector3 direction = target.transform.position - _enemyBase.transform.position;
            direction.y = 0;

            //플레이어 발견했고 그 사이에 장애물도 없다.
            if (_enemyBase.IsObstacleInLine(direction.magnitude, direction.normalized) == false)
            {
                _enemyBase.targetTrm = target.transform;
                _stateMachine.ChangeState(KnifeEnum.Attack);//전투상태로 전환
            }
        }
    }
}
