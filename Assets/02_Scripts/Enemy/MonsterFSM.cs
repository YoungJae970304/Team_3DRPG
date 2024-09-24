using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFSM
{
    private MonsterBaseState _curState;

    public MonsterFSM(MonsterBaseState initState)
    {
        _curState = initState;
        ChangeState(_curState);
    }

    public void ChangeState(MonsterBaseState nextState)
    {
        if (nextState == _curState)
            return;
        if (_curState != null)
            _curState.OnStateExit();
        _curState = nextState;
        _curState.OnStateEnter();
    }

    public void UpdateState()
    {
        if (_curState != null)
            _curState.OnStateUpdate();
    }
}
