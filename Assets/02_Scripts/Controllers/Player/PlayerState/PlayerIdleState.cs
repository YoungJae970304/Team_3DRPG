using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : BaseState
{
    public PlayerIdleState(Player player, Monster monster, ITotalStat stat) : base(player, monster, stat) { }

    public override void OnStateEnter()
    {
        _player.AtkCount = 0;
    }

    public override void OnStateUpdate()
    {

    }

    public override void OnStateExit()
    {
        Logger.Log("대기상태 Exit");
    }
}
