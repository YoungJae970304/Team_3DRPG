using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrkDieState : MonsterBaseState
{
    public OrkDieState(Ork ork) : base(ork) 
    {
        _ork = ork;
    }

    public override void OnStateEnter()
    {
        //죽는 모션
        _ork.StartCoroutine(_ork.DropItem(_monster._sName = Monster.StageName.Hard,_ork.transform));
        OnStateExit();
    }

    public override void OnStateExit()
    {
        //exit가 필요할까
        //_ork.OrkDie();
    }

    public override void OnStateUpdate()
    {
        //엑시트 위치 생각하기
    }

  
}
