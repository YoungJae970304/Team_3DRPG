using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSkillState : BaseState
{
    public MonsterSkillState(Player player, Monster monster, Stat stat) : base(player, monster, stat)
    {
    }

    public override void OnStateEnter()
    {
        
    }

    public override void OnStateExit()
    {
        _monster._mAttackState = MAttackState.NomalAttack;
    }

    public override void OnStateUpdate()
    {
        
    }

}
