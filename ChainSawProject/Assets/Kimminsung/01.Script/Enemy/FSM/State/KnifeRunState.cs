using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace MIN
{
    public class KnifeRunState : EnemyState<KnifeEnum>
    {
        private NavMeshAgent _navMeshAgent;

        public KnifeRunState(Enemy enemyBase, EnemyStateMachine<KnifeEnum> stateMachine, string animBoolName)
            : base(enemyBase, stateMachine, animBoolName)
        {
            _navMeshAgent = _enemyBase.GetComponent<NavMeshAgent>();
        }

        public override void Enter()
        {
            base.Enter();
            _navMeshAgent.speed = _enemyBase.runAwayDistance;  // Set the speed for running
            _navMeshAgent.isStopped = false;  // Ensure the NavMeshAgent is active
        }

        public override void UpdateState()
        {
            base.UpdateState();
            Debug.Log("?");
            Collider2D target = _enemyBase.IsPlayerDetected();
            if (target == null)
            {
                _navMeshAgent.isStopped = true;  // Stop moving if no player is detected
                return;
            }

            Vector2 direction = target.transform.position - _enemyBase.transform.position;
            direction.y = 0;  // Ensure the enemy only moves on the XZ plane

            if (!_enemyBase.IsObstacleInLine(direction.magnitude, direction.normalized))
            {
                _enemyBase.targetTrm = target.transform;
                _navMeshAgent.SetDestination(target.transform.position);  // Chase the player

                // Optionally check if close enough to attack
                if (Vector3.Distance(_enemyBase.transform.position, target.transform.position) <= _enemyBase.attackDistance)
                {
                    _stateMachine.ChangeState(KnifeEnum.Attack);  // Switch to attack state when close enough
                }
            }
            else
            {
                _navMeshAgent.isStopped = true;  // Stop moving if there is an obstacle
            }
        }

        public override void Exit()
        {
            base.Exit();
            _navMeshAgent.isStopped = true;  // Stop the NavMeshAgent when exiting the state
        }
    }

}
