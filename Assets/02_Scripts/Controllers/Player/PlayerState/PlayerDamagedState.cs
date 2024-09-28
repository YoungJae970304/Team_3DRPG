using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamagedState : PlayerBaseState
{
    public PlayerDamagedState(Player player) : base(player) { }

    public override void OnStateEnter()
    {
        _player._hitting = true;
    }
    public override void OnStateUpdate()
    {

    }

    public override void OnStateExit()
    {
        _player._hitting = false;
    }

}
