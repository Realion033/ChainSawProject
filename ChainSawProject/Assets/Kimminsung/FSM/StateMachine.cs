using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private State currentState;

    public Transform playerTransform;

    void Start()
    {
        // 초기 상태 설정, 플레이어의 트랜스폼을 전달
        ChangeState(new IdleState(gameObject, this, playerTransform));
    }

    void Update()
    {
        currentState?.Update();
    }

    public void ChangeState(State newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }
}