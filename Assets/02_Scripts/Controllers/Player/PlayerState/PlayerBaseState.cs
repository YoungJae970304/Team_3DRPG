using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState
{
    protected Player _player;

    protected PlayerBaseState(Player player)
    {
        _player = player;
    }

    public abstract void OnStateEnter();
    public abstract void OnStateUpdate();
    public virtual void OnStateFixedUpdate() { }
    public abstract void OnStateExit();
}
