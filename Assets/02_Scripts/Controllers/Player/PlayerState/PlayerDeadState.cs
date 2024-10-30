using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : BaseState
{
    public PlayerDeadState(Player player, Monster monster, ITotalStat stat) : base(player, monster, stat) { }
    public override void OnStateEnter()
    {
        Logger.Log("플레이어 사망");
        _player._playerAnim.SetTrigger("doDead");
        _player._cc.enabled = false;
        _player.enabled = false;
    }

    public override void OnStateUpdate()
    {
        
    }

    public override void OnStateExit()
    {
        _player._cc.enabled = true;
        _player.enabled = true;
    }
}
