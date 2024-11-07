using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : BaseState
{
    public PlayerIdleState(Player player, Monster monster, ITotalStat stat) : base(player, monster, stat) { }

    public override void OnStateEnter()
    {
        _player._canAtkInput = true;
        _player._attacking = false;
        _player._dodgeing = false;
        _player.AtkCount = 0;
        _player._hitMobs.Clear();
        _player._rotDir = _player._playerModel.forward;

        _player._playerAnim.Rebind();
    }

    public override void OnStateUpdate()
    {

    }

    public override void OnStateExit()
    {
    }
}
