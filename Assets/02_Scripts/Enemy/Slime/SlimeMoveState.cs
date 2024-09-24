using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMoveState : MonsterBaseState
{
    public SlimeMoveState(Slime slime) : base(slime) { }
    float _timer = 0;
    public override void OnStateEnter()
    {
        //플레이어 찾기(슬라임에서 찾아둠)
        _slime._nav.ResetPath();
        _slime._nav.stoppingDistance = _slime._sStat.AttackRange;
        _slime._nav.destination = _slime._player.transform.position;
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
        //얘도 슬라임이랑 비교해서 조건 확인하기
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
