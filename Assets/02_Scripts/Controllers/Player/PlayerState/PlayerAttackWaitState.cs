using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackWaitState : BaseState
{
    public PlayerAttackWaitState(Player player, Monster monster, ITotalStat stat) : base(player, monster, stat) { }

    public override void OnStateEnter()
    {

    }

    public override void OnStateUpdate()
    {

    }

    public override void OnStateExit()
    {
        Logger.Log("공격 대기 상태 Exit");
        if (_player._playerInput._atkInput.Count < 1)
        {
            _player._attacking = false;
            _player._playerAnim.SetBool("isAttacking", false);
        }
    }
}
