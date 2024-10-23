using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusEffectState : BaseState
{
    public PlayerStatusEffectState(Player player, Monster monster, ITotalStat stat) : base(player, monster, stat) { }

    public override void OnStateEnter()
    {
        // 스턴 애니메이션 재생
        _player._playerAnim.SetTrigger("doStun");
    }

    public override void OnStateUpdate()
    {
       
    }

    public override void OnStateExit()
    {
        
    }
}
