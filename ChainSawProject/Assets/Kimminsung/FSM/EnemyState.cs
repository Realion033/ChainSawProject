using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EnemyState : MonoBehaviour
{
    protected Enemy_Knife _owner;
    protected StateMachine stateMachine;

    public EnemyState(Enemy_Knife enemy, StateMachine stateMachine)
    {
        _owner = enemy;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter()
    {
    }

    public virtual void Update() { }
    public virtual void Exit() { }
}
