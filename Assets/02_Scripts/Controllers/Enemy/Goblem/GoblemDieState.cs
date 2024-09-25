using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblemDieState: MonsterBaseState
{
    public GoblemDieState(Goblem goblem) : base(goblem)
    {
        _goblem = goblem;
    }

    public override void OnStateEnter()
    {
        //죽는 모션
        _goblem.DropItem();
        OnStateExit();
    }

    public override void OnStateExit()
    {
        //exit가 필요할까
        _goblem.GoblemDie();
    }

    public override void OnStateUpdate()
    {
        //엑시트 위치 생각하기
    }

}
