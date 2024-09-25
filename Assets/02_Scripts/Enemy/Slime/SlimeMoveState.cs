using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMoveState : MonsterBaseState
{
    public SlimeMoveState(Slime slime) : base(slime) 
    {
        _slime = slime;
    }
    float _timer = 0;
    SlimeStat _sStat;
    public override void OnStateEnter()
    {
        //플레이어 찾기(슬라임에서 찾아둠)
        _slime._nav.stoppingDistance = _sStat.AttackRange;
        _slime._nav.destination = _slime._player.transform.position;
    }

    public override void OnStateExit()
    {

    }

    public override void OnStateUpdate()
    {
        //플레이어 추격
        _slime._nav.SetDestination(_slime._nav.destination);
        _timer += Time.deltaTime;
        if(_timer > 2f)
        {
            _slime._nav.destination = _slime._player.transform.position;
        }
    }

}
