using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeDieState : MonsterBaseState
{
    public SlimeDieState(Slime slime) : base(slime) { }

    public override void OnStateEnter()
    {
        //죽는 모션
        OnStateUpdate();
    }

    public override void OnStateExit()
    {
        //exit가 필요할까
        _slime.SlimeDie();
    }

    public override void OnStateUpdate()
    {
        //아이템 떨구기
        _slime.DropItem();
        OnStateExit();
    }
}
