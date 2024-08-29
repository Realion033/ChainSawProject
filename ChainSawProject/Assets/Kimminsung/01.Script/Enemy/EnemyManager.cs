using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private enum State  
    {
        Idle,
        Move,
        Attack
    }
    private State _state;

    private void Start()
    {
        _state = State.Idle;
    }

    private void Update()
    {
        switch(_state)
        {
            case State.Idle:
                // IdleState상태때 해야할 것
                break;
            case State.Move:
                //MoveState상태때 해야할 것
                break;
            case State.Attack:
                //AttackState상태때 해야할 것
                break;
        }
    }
}
