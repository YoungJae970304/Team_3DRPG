using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMoveState : MonsterBaseState
{
    public SlimeMoveState(Slime slime) : base(slime) { }

    public override void OnStateEnter()
    {
        //플레이어 찾기(슬라임에서 찾아둠)
        _slime._nav.ResetPath();
        _slime._nav.stoppingDistance = _slime._mStat.AttackRange;
        _slime._nav.destination = _slime._player.transform.position;
        OnStateUpdate();
    }

    public override void OnStateExit()
    {
        //다른 상태로 변환
        if (_slime.ReturnOrigin())
        {
            _slime.ChangeState(Slime.State.Return);
        }
        else if (_slime.CanAttackPlayer())
        {
            _slime.ChangeState(Slime.State.Attack);
        }
        else if (_slime.DamageToPlayer())
        {
            _slime.ChangeState(Slime.State.Move);
        }
    }

    public override void OnStateUpdate()
    {
        //플레이어 추격
        _slime._nav.Move(_slime._player.transform.position);
        OnStateExit();
    }

}
