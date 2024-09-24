using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblemIdleState : MonsterBaseState
{
    public GoblemIdleState(Goblem goblem) : base(goblem)
    {
        _goblem = goblem;
    }
    GoblemStat _gStat;
    float awayRangeX;
    //float awayRangeY = Random.Range(0, _sStat.AwayRange);
    float awayRangeZ;
    public override void OnStateEnter()
    {
        _gStat = _goblem.GetComponent<GoblemStat>();
        if (_gStat == null)
        {
            Debug.LogError("SlimeStat 컴포넌트를 찾을 수 없습니다.");
        }
        awayRangeX = Random.Range(-_gStat.AwayRange, _gStat.AwayRange);
        //float awayRangeY = Random.Range(0, _sStat.AwayRange);
        awayRangeZ = Random.Range(-_gStat.AwayRange, _gStat.AwayRange);
        _goblem._nav.destination = _goblem._originPos + new Vector3(awayRangeX, 0, awayRangeZ);
    }

    public override void OnStateExit()
    {
        //얘를 어떻게 해야할까
    }

    public override void OnStateUpdate()
    {
        if (_gStat == null) return;
        //일정 거리 배회
        //선공몹들은 플레이어가 일정 거리 안에 들어온다면 Exit로 상태 변환
        awayRangeX = Random.Range(-_gStat.AwayRange, _gStat.AwayRange);
        //float awayRangeY = Random.Range(0, _sStat.AwayRange);
        awayRangeZ = Random.Range(-_gStat.AwayRange, _gStat.AwayRange);

        if ((_goblem._originPos + _goblem.transform.position).magnitude < (_goblem._originPos).magnitude + _gStat.ReturnRange ||
            (_goblem._originPos - _goblem.transform.position).magnitude > (_goblem._originPos).magnitude - _gStat.ReturnRange)
        {
            if ((_goblem._nav.destination - _goblem.transform.position).magnitude > 1f)
            {
                _goblem._nav.SetDestination(_goblem._nav.destination);
            }
            else if (_goblem._curState == Goblem.State.Move)
            {
                _goblem._nav.destination = _goblem._player.transform.position;
            }
            else
            {
                _goblem._nav.destination = _goblem._originPos + new Vector3(awayRangeX, 0, awayRangeZ);
            }
        }
        else
        {
            //오리진 포스에서 일정 범위 이상으로 벗어났다면 return하기 - 근데 얜 slime에서 변경되야함
            _goblem._nav.destination = _goblem._originPos;
        }
    }
}
