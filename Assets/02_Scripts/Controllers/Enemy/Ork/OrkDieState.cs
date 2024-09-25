using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrkDieState : MonsterBaseState
{
    public OrkDieState(Ork ork) : base(ork) { }

    public override void OnStateEnter()
    {
        //죽는 모션
        _ork.DropItem();
        OnStateExit();
    }

    public override void OnStateExit()
    {
        //exit가 필요할까
        _ork.OrkDie();
    }

    public override void OnStateUpdate()
    {
        //엑시트 위치 생각하기
    }

}
