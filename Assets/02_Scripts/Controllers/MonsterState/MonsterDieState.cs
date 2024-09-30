using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDieState : BaseState
{
    public MonsterDieState(Player player, Monster monster, Stat stat) : base(player, monster, stat)
    {
    }

    public override void OnStateEnter()
    {
        Logger.Log("몬스터 사망");
        _monster.Die(_monster.gameObject);
    }

    public override void OnStateExit()
    {
        
    }

    public override void OnStateUpdate()
    {
        
    }
}
