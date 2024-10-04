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
        _monster._nav.enabled = false;
        _monster.GetComponent<BoxCollider>().enabled = false;
        _monster._monsterDrop.DropItemSelect(_monster._deongeonLevel, _monster.sample);//임시 설정 추후 던전에서 받아오도록 변경
        _monster.Die(_monster.gameObject);
    }
    //
    public override void OnStateExit()
    {
        
    }

    public override void OnStateUpdate()
    {
        
    }
}
