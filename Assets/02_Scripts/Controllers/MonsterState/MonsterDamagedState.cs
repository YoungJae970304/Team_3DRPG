using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDamagedState : BaseState
{
    public MonsterDamagedState(Player player, Monster monster, Stat stat) : base(player, monster, stat) { }
    public override void OnStateEnter()
    {
        //넉백, 데미지 받기
        //임시로 몬스터의 데미지를 넣어놓음 추후 플레이어 데미지 값 받아오게 설정
        _monster._nav.enabled = false;
        _monster.GetComponent<Rigidbody>().isKinematic = false;
        _monster.StartCoroutine(_monster.StartDamege( _player.transform.position, 0.1f, 10f));
    }

    public override void OnStateExit()
    {
        //코루틴 멈추기?
        //상태는 냅둿다가 나중에 슬라임 스위치 데미지파트랑 비교하기
        _monster._nav.enabled = true;
        _monster.GetComponent<Rigidbody>().isKinematic = true;
    }

    public override void OnStateUpdate()
    {
        
    }

    
}
