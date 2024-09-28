using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState
{
    protected Player _player;
    protected PlayerStat _playerStat;

    protected PlayerBaseState(Player player)
    {
        _player = player;
        _playerStat = player._playerStat;
    }

    public abstract void OnStateEnter();
    public abstract void OnStateUpdate();
    public virtual void OnStateFixedUpdate() { }
    public abstract void OnStateExit();
}
