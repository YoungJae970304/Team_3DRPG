using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMoveState : BaseState
{
    public MonsterMoveState(Player player, Monster monster, Stat stat) : base(player, monster, stat)
    {
    }

    float _timer = 0;
    public override void OnStateEnter()
    {
        //플레이어 찾기(슬라임에서 찾아둠)
        _monster._nav.stoppingDistance = _monster._mStat.AttackRange;
        _monster._nav.destination = _monster._player.transform.position;
    }

    public override void OnStateExit()
    {
        _monster._nav.stoppingDistance = 0;
    }

    public override void OnStateUpdate()
    {
        //플레이어 추격
        _monster._nav.SetDestination(_monster._nav.destination);
        _timer += Time.deltaTime;
        if (_timer > 2f)
        {
            _monster._nav.destination = _monster._player.transform.position;
        }
    }
}
