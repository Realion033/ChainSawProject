using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState
{
    protected EnemyManager _enemy;

    protected BaseState(EnemyManager enemy)
    {
        _enemy = enemy;
    }

    public abstract void OnStateEnter();// ���¿� �������� �� �ѹ��� ȣ���
    public abstract void OnStateUpdate(); // �� ������ ���� ȣ��� 
    public abstract void OnStateExit(); // ���°� ����Ǹ� ȣ���
}
