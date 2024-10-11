using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterIdleState : BaseState
{
    public MonsterIdleState(Player player, Monster monster, Stat stat) : base(player, monster, stat)
    {

    }
    float awayRangeX;
    //float awayRangeY = Random.Range(0, _sStat.AwayRange);
    float awayRangeZ;
    public override void OnStateEnter()
    {
        _monster._mStat = _monster.GetComponent<MonsterStat>();
        if (_monster._mStat == null)
        {
            Debug.LogError("OrkStat 컴포넌트를 찾을 수 없습니다.");
        }
        awayRangeX = Random.Range(-_monster._mStat.AwayRange, _monster._mStat.AwayRange);
        //float awayRangeY = Random.Range(0, _sStat.AwayRange);
        awayRangeZ = Random.Range(-_monster._mStat.AwayRange, _monster._mStat.AwayRange);
        _monster._nav.destination = _monster._originPos + new Vector3(awayRangeX, 0, awayRangeZ);
    }

    public override void OnStateExit()
    {
        //얘를 어떻게 해야할까

        //_monster._nav.destination = _monster._player.transform.position;

    }

    public override void OnStateUpdate()
    {
        if (_monster._mStat == null) return;
        //일정 거리 배회
        //선공몹들은 플레이어가 일정 거리 안에 들어온다면 Exit로 상태 변환
        awayRangeX = Random.Range(-_monster._mStat.AwayRange, _monster._mStat.AwayRange);
        //float awayRangeY = Random.Range(0, _sStat.AwayRange);
        awayRangeZ = Random.Range(-_monster._mStat.AwayRange, _monster._mStat.AwayRange);

        if ((_monster._nav.destination - _monster.transform.position).magnitude > 1f)
        {
            _monster._nav.SetDestination(_monster._nav.destination);
        }

        else
        {
            _monster._nav.destination = _monster._originPos + new Vector3(awayRangeX, 0, awayRangeZ);
        }

    }
}
