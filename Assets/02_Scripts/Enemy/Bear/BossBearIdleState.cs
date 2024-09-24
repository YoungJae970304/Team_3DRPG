using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBearIdleState : MonsterBaseState
{
    public BossBearIdleState(BossBear bossBear) : base(bossBear)
    {
        _bossBear = bossBear;
    }
    BearStat _bStat;
    float awayRangeX;
    //float awayRangeY = Random.Range(0, _sStat.AwayRange);
    float awayRangeZ;
    public override void OnStateEnter()
    {
        _bStat = _bossBear.GetComponent<BearStat>();
        if (_bStat == null)
        {
            Debug.LogError("SlimeStat 컴포넌트를 찾을 수 없습니다.");
        }
        awayRangeX = Random.Range(-_bStat.AwayRange, _bStat.AwayRange);
        //float awayRangeY = Random.Range(0, _sStat.AwayRange);
        awayRangeZ = Random.Range(-_bStat.AwayRange, _bStat.AwayRange);
        _bossBear._nav.destination = _bossBear._originPos + new Vector3(awayRangeX, 0, awayRangeZ);
    }

    public override void OnStateExit()
    {
        //얘를 어떻게 해야할까
    }

    public override void OnStateUpdate()
    {
        if (_bStat == null) return;
        //일정 거리 배회
        //선공몹들은 플레이어가 일정 거리 안에 들어온다면 Exit로 상태 변환
        awayRangeX = Random.Range(-_bStat.AwayRange, _bStat.AwayRange);
        //float awayRangeY = Random.Range(0, _sStat.AwayRange);
        awayRangeZ = Random.Range(-_bStat.AwayRange, _bStat.AwayRange);

        if ((_bossBear._originPos + _bossBear.transform.position).magnitude < (_bossBear._originPos).magnitude + _bStat.ReturnRange ||
            (_bossBear._originPos - _bossBear.transform.position).magnitude > (_bossBear._originPos).magnitude - _bStat.ReturnRange)
        {
            if ((_bossBear._nav.destination - _bossBear.transform.position).magnitude > 1f)
            {
                _bossBear._nav.SetDestination(_bossBear._nav.destination);
            }
            else if (_bossBear._curState == BossBear.State.Move)
            {
                _bossBear._nav.destination = _bossBear._player.transform.position;
            }
            else
            {
                _bossBear._nav.destination = _bossBear._originPos + new Vector3(awayRangeX, 0, awayRangeZ);
            }
        }
        else
        {
            //오리진 포스에서 일정 범위 이상으로 벗어났다면 return하기 - 근데 얜 slime에서 변경되야함
            _bossBear._nav.destination = _bossBear._originPos;
        }
    }
}
