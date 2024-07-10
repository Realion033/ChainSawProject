using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private State currentState;

    public Transform playerTransform;

    void Start()
    {
        // �ʱ� ���� ����, �÷��̾��� Ʈ�������� ����
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