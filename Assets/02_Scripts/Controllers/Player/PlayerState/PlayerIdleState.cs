using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : BaseState
{
    public PlayerIdleState(Player player, Monster monster, ITotalStat stat) : base(player, monster, stat) { }

    public override void OnStateEnter()
    {
        _player.AtkCount = 0;
        _player._rotDir = _player._playerModel.forward;
    }

    public override void OnStateUpdate()
    {

    }

    public override void OnStateExit()
    {
    }
}
