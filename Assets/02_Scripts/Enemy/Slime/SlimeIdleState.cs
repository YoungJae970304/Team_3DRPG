using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class SlimeIdleState : MonsterBaseState
{
    public SlimeIdleState(Slime slime) : base(slime) { }
    Vector3 _originPos;
    public override void OnStateEnter()
    {
        //현재 위치 저장
        _originPos = _monster.transform.position;
        OnStateUpdate();
    }

    public override void OnStateExit()
    {
        _slime.ChangeState(Slime.State.Damage);
    }

    public override void OnStateUpdate()
    {
        //일정 거리 배회
        //슬라임은 계속 배회
        //선공몹들은 플레이어가 일정 거리 안에 들어온다면 Exit로 상태 변환
        float awayRangeX = Random.Range(0, _monster._mStat.AwayRange + 1);
        float awayRangeY = Random.Range(0, _monster._mStat.AwayRange + 1);
        float awayRangeZ = Random.Range(0, _monster._mStat.AwayRange + 1);
        if ((_originPos + _monster.transform.position).magnitude < (_originPos).magnitude + _monster._mStat.ReturnRange)
        {
            _monster._nav.Move(_originPos + new Vector3(awayRangeX, awayRangeY, awayRangeZ));
        }
        else
        {
            OnStateExit();
        }
    }
}