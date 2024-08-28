using System.Runtime.Serialization;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private EnemyState currentState;

    private Enemy_Knife enemy;

    public void Initialize(Enemy_Knife enemy)
    {
        this.enemy = enemy;
    }

    void Awake()
    {
<<<<<<< HEAD:ChainSawProject/Assets/Kimminsung/StateMachine.cs
        ChangeState(new IdleState(gameObject, this, playerTransform));
=======
        // �ʱ� ���� ����, �÷��̾��� Ʈ�������� ����
        ChangeState(new IdleState(enemy, this));
>>>>>>> Kiminsung:ChainSawProject/Assets/Kimminsung/FSM/StateMachine.cs
    }
    
    void Update()
    {
        currentState?.Update();
    }

    public void ChangeState(EnemyState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }
}