using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace MIN
{
    public class KnifeRunState : EnemyState<KnifeEnum>
    {
        public float moveSpeed = 5f; // 적의 이동 속도

        public KnifeRunState(Enemy enemyBase, EnemyStateMachine<KnifeEnum> stateMachine, string animBoolName)
            : base(enemyBase, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
        }

        public override void UpdateState()
        {
            base.UpdateState();
            Debug.Log("?");
            Collider2D target = _enemyBase.IsPlayerDetected();
            if (target == null)
            {
                return; 
            }

            Vector2 direction = target.transform.position - _enemyBase.transform.position;
            direction.y = 0;  

            if (!_enemyBase.IsObstacleInLine(direction.magnitude, direction.normalized))
            {
                _enemyBase.targetTrm = target.transform;
                
                Vector2 moveDirection = direction.normalized * moveSpeed * Time.deltaTime;
                _enemyBase.transform.Translate(moveDirection, Space.World);

                
                if (Vector3.Distance(_enemyBase.transform.position, target.transform.position) <= _enemyBase.attackDistance)
                {
                    _stateMachine.ChangeState(KnifeEnum.Attack);  
                }
            }
        }

        public override void Exit()
        {
            base.Exit();
            // 상태 종료 시 필요한 처리를 여기에 작성
        }
    }
}
