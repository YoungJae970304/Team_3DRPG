using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterIdleState : BaseState
{
    public MonsterIdleState(Player player, Monster monster, ITotalStat stat) : base(player, monster, stat)
    {

    }
    float awayRangeX;
    //float awayRangeY = Random.Range(0, _sStat.AwayRange);
    float awayRangeZ;
    public override void OnStateEnter()
    {

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
        _monster.SetDestinationOnTimeToIdle(1);

    }
}
