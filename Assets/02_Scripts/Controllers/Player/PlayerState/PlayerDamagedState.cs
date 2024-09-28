using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamagedState : BaseState
{
    public PlayerDamagedState(Player player, Monster monster, Stat stat) : base(player, monster, stat) { }

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
