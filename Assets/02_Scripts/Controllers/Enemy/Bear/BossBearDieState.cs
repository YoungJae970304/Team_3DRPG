using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBearDieState : MonsterBaseState
{
    public BossBearDieState(BossBear bossBear) : base(bossBear)
    {
        _bossBear = bossBear;
    }

    public override void OnStateEnter()
    {
        //죽는 모션
        _bossBear.DropItem();
        OnStateExit();
    }

    public override void OnStateExit()
    {
        //exit가 필요할까
        _bossBear.BossBearDie();
    }

    public override void OnStateUpdate()
    {
        //엑시트 위치 생각하기
    }
}
