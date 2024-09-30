using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackWaitState : BaseState
{
    public PlayerAttackWaitState(Player player, Monster monster, Stat stat) : base(player, monster, stat) { }

    public override void OnStateEnter()
    {
        _player._curATime = 0;
    }

    public override void OnStateUpdate()
    {
        _player.Attack();
    }

    public override void OnStateExit()
    {
        Logger.Log("공격 대기 상태 Exit");
    }
}
