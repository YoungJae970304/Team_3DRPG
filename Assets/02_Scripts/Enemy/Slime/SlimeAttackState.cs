using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SlimeAttackState : MonsterBaseState
{
    public SlimeAttackState(Slime slime) : base(slime) { }
    
    
    public override void OnStateEnter()
    {
        //플레이어 공격
        
        //_slime._player.Damaged(_slime._mStat.Attack);
        //애니메이션 실행
       
    }

    public override void OnStateExit()
    {
        if(_slime._mStat.Hp <= 0)
        {
            _slime.ChangeState(Slime.State.Die);
        }
        else
        {
            _slime.ChangeState(Slime.State.Move);
        }
    }

    public override void OnStateUpdate()
    {
        //딜레이 후 플레이어 공격
        if(_slime._timer > _slime._attackDelay)
        {
            _slime._timer = 0f;
            //_slime._player.Damaged(_slime._mStat.Attack);
            //애니메이션 실행
            OnStateExit();
        }
    }
}
