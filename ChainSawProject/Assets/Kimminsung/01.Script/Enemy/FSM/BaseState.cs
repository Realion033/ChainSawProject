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

    public abstract void OnStateEnter();// 상태에 진입했을 때 한번만 호출됨
    public abstract void OnStateUpdate(); // 매 프레임 마다 호출됨 
    public abstract void OnStateExit(); // 상태가 변경되면 호출됨
}
