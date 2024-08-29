using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeEnemy : EnemyManager
{
    private enum State
    {
        Idle,
        Move,
        Attack
    }

    Transform _transform;
    GameObject _player;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _player = GetComponent<GameObject>();
    }

    private State _curState;
    private FSM _fsm;

    private void Start()
    {
        _curState = State.Idle;
        _fsm = new FSM(new IdleState(this));
    }

    private void Update()
    {
        switch (_curState)
        {
            case State.Idle:
                if (CanSeePlayer())
                {
                    if (CanAttackPlayer())
                        ChangeState(State.Attack);
                    else
                        ChangeState(State.Move);
                }
                break;
            case State.Move:
                if (CanSeePlayer())
                {
                    if (!CanAttackPlayer())
                    {
                        ChangeState(State.Attack);
                    }
                }
                else
                {
                    ChangeState(State.Idle);
                }
                break;
            case State.Attack:
                if (CanSeePlayer())
                {
                    if (!CanAttackPlayer())
                    {
                        ChangeState(State.Move);
                    }
                }
                else

                {
                    ChangeState(State.Idle);
                }

                break;
        }
        _fsm.UpdateState();
    }


    private void ChangeState(State nextState)
    {
        _curState = nextState;
        switch (_curState)
        {
            case State.Idle:
                _fsm.ChangeState(new IdleState(this));
                break;
            case State.Move:
                _fsm.ChangeState(new MoveState(this));
                break;
            case State.Attack:
                _fsm.ChangeState(new AttackState(this));
                break;
        }
    }


    private bool CanSeePlayer()
    {
        return true;
    }

    private bool CanAttackPlayer()
    {
        return true;
    }
}
