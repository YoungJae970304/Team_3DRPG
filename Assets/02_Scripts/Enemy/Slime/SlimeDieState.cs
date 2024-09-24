using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeDieState : MonsterBaseState
{
    public SlimeDieState(Slime slime) : base(slime) { }

    public override void OnStateEnter()
    {
        //죽는 모션
        _slime.DropItem();
        OnStateExit();
    }

    public override void OnStateExit()
    {
        //exit가 필요할까
        _slime.SlimeDie();
    }

    public override void OnStateUpdate()
    {    
        //엑시트 위치 생각하기
    }
}
