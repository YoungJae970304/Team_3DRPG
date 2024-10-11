using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSkillState : BaseState
{
    public MonsterSkillState(Player player, Monster monster, ITotalStat stat) : base(player, monster, stat)
    {
    }

    public override void OnStateEnter()
    {
        
    }

    public override void OnStateExit()
    {
       
        _player._playerHitState = PlayerHitState.NomalAttack;
    }

    public override void OnStateUpdate()
    {
        
    }

}
