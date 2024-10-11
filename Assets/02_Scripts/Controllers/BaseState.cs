using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState
{
    protected Player _player;
    protected Monster _monster;
    protected ITotalStat _stat;

    protected BaseState(Player player, Monster monster, ITotalStat stat)
    {
        _player = player;
        _monster = monster;
        _stat = stat;
    }

    public abstract void OnStateEnter();
    public abstract void OnStateUpdate();
    public virtual void OnStateFixedUpdate() { }
    public abstract void OnStateExit();
}
