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
        _player._canAtkInput = false;
        _player._curAtkCount = _player._playerInput._atkInput.Dequeue();
    }

    public override void OnStateUpdate()
    {
        _player.Attack();
    }

    public override void OnStateExit()
    {
        Logger.Log("공격 상태 Exit ");
        _player._attacking = false;
        _player._canAtkInput = true;
        _player.AtkCount = 0;
    }
}
