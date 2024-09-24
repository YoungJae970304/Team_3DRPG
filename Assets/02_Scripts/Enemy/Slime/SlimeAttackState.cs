using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SlimeAttackState : MonsterBaseState
{
    public SlimeAttackState(Slime slime) : base(slime) { }
    
    
    public override void OnStateEnter()
    {
        //�÷��̾� ����
        
        //_slime._player.Damaged(_slime._mStat.Attack);
        //�ִϸ��̼� ����
       
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
        //������ �� �÷��̾� ����
        if(_slime._timer > _slime._attackDelay)
        {
            _slime._timer = 0f;
            //_slime._player.Damaged(_slime._mStat.Attack);
            //�ִϸ��̼� ����
            OnStateExit();
        }
    }
}
