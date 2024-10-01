using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamagedState : BaseState
{
    public PlayerDamagedState(Player player, Monster monster, Stat stat) : base(player, monster, stat) { }

    public override void OnStateEnter()
    {
        // 이후 몬스터의 넉백 공격, 기절 공격 등 다른 조건에 따라 이 상태로 진입
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
