using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    public PlayerAttackState(Player player) : base(player) 
    {
        _player = player;
    }

    public override void OnStateEnter()
    {
        Logger.Log("공격 상태 진입");
        _player._attacking = true;
    }

    public override void OnStateUpdate()
    {
        Logger.Log("공격 상태 업데이트");
        _player.Attack();
    }

    public override void OnStateExit()
    {
        Logger.Log("공격 상태 Exit");
    }
}
