using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeReturnState : MonsterBaseState
{
    public SlimeReturnState(Slime slime) : base(slime) { }

    public override void OnStateEnter()
    {
        //origin포스 찾아서 이동하기
        _slime._nav.Move(_slime._originPos);
        OnStateUpdate();
    }

    public override void OnStateExit()
    {
        //originPos라면 Idle로 상태 변환
        //아니라면 플레이어 추격(Move로 상태 변환)
        if ((_slime._originPos - _slime.transform.position).magnitude <= 0.1f)
        {
            _slime.ChangeState(Slime.State.Idle);
        }
    }

    public override void OnStateUpdate()
    {
        //다시 맞았을 때 위치 판단해서 return거리보다 작아졌다면 플레이어 추격
        OnStateExit();
    }
}
