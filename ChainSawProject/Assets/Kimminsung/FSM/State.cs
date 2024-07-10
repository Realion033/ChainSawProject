using UnityEngine;

public abstract class State
{
    protected GameObject gameObject;
    protected StateMachine stateMachine;

    public State(GameObject gameObject, StateMachine stateMachine)
    {
        this.gameObject = gameObject;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Exit() { }
}