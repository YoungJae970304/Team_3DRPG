using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeDamagedState : MonsterBaseState
{
    public SlimeDamagedState(Slime slime) : base(slime) { }
    public override void OnStateEnter()
    {
        //넉백, 데미지 받기
        //임시로 몬스터의 데미지를 넣어놓음 추후 플레이어 데미지 값 받아오게 설정
        _slime.StartCoroutine(_slime.StartDamege(_monster._mStat.Attack, _slime.transform.position, 0.5f, 0.5f));
        OnStateUpdate();
    }

    public override void OnStateExit()
    {
        //다른 상태로 변환
        
        
        if(_slime.ReturnOrigin())
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
        //업데이트에 들어갈게 있나?
        OnStateExit();
    }
    
}
