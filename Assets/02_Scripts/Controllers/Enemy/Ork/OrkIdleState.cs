using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrkIdleState : MonsterBaseState
{
    public OrkIdleState(Ork ork) : base(ork)
    {
        _ork = ork;
        _oStat = _ork._oStat;
    }
    OrkStat _oStat;
    float awayRangeX;
    //float awayRangeY = Random.Range(0, _sStat.AwayRange);
    float awayRangeZ;
    public override void OnStateEnter()
    {
        _oStat = _ork.GetComponent<OrkStat>();
        if (_oStat == null)
        {
            Debug.LogError("OrkStat 컴포넌트를 찾을 수 없습니다.");
        }
        awayRangeX = Random.Range(-_oStat.AwayRange, _oStat.AwayRange);
        //float awayRangeY = Random.Range(0, _sStat.AwayRange);
        awayRangeZ = Random.Range(-_oStat.AwayRange, _oStat.AwayRange);
        _ork._nav.destination = _ork._originPos + new Vector3(awayRangeX, 0, awayRangeZ);
    }

    public override void OnStateExit()
    {
        //얘를 어떻게 해야할까
    }

    public override void OnStateUpdate()
    {
        if (_oStat == null) return;
        //일정 거리 배회
        //선공몹들은 플레이어가 일정 거리 안에 들어온다면 Exit로 상태 변환
        awayRangeX = Random.Range(-_oStat.AwayRange, _oStat.AwayRange);
        //float awayRangeY = Random.Range(0, _sStat.AwayRange);
        awayRangeZ = Random.Range(-_oStat.AwayRange, _oStat.AwayRange);

        if ((_ork._originPos + _ork.transform.position).magnitude < (_ork._originPos).magnitude + _oStat.ReturnRange ||
            (_ork._originPos - _ork.transform.position).magnitude > (_ork._originPos).magnitude - _oStat.ReturnRange)
        {
            if ((_ork._nav.destination - _ork.transform.position).magnitude > 1f)
            {
                _ork._nav.SetDestination(_ork._nav.destination);
            }
            else if (_ork._curState == Ork.State.Move)
            {
                _ork._nav.destination = _ork._player.transform.position;
            }
            else
            {
                _ork._nav.destination = _ork._originPos + new Vector3(awayRangeX, 0, awayRangeZ);
            }
        }
        else
        {
            //오리진 포스에서 일정 범위 이상으로 벗어났다면 return하기 - 근데 얜 slime에서 변경되야함
            _ork._nav.destination = _ork._originPos;
        }
        Logger.Log(_ork._nav.destination.ToString());
    }
}
