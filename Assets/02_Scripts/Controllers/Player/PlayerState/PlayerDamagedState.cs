using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamagedState : BaseState
{
    public PlayerDamagedState(Player player, Monster monster, ITotalStat stat) : base(player, monster, stat) { }

    public override void OnStateEnter()
    {
        _player._playerAnim.SetTrigger("doDamaged");
        _player._hitting = true;
        Logger.Log("플레이어 움찔");
    }
    public override void OnStateUpdate()
    {

    }

    public override void OnStateExit()
    {
        _player._hitting = false;
        _player._invincible = false;
        _player._canAtkInput = true;
        _player._attacking = false;
    }
}
