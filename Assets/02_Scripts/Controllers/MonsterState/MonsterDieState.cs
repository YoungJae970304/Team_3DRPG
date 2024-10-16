using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MonsterDieState : BaseState
{
    public MonsterDieState(Player player, Monster monster, ITotalStat stat) : base(player, monster, stat)
    {
    }

    public override void OnStateEnter()
    {
        _monster._anim.SetTrigger("Die");
        _monster._nav.enabled = false;
        Logger.Log("몬스터 사망");
        //_monster.GetComponent<BoxCollider>().enabled = false;
        _monster._monsterDrop.DropItemSelect(_monster._deongeonLevel, _monster.sample);//임시 설정 추후 던전에서 받아오도록 변경
        _monster.MakeItem();
        _monster.Die(_monster.gameObject);

        // 영재 : 임시로 죽었을 때 게임매니저에서 제거하는 부분 추가
        Managers.Game._monsters.Remove(_monster);
    }
    //
    public override void OnStateExit()
    {
        
    }

    public override void OnStateUpdate()
    {
        
    }
}
