using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SlimeAttackState : MonsterBaseState
{
    public SlimeAttackState(Slime slime) : base(slime) 
    {
        _slime = slime;
    }
    float _timer = 0f;

    public override void OnStateEnter()
    {
        //�÷��̾� ����
        
        //_slime._player.Damaged(_slime._mStat.Attack);
        //�ִϸ��̼� ����
       
    }

    public override void OnStateExit()
    {
        //���� ���� ���� �ʱ�ȭ �̰� ����� �����ؾ��ҵ�
    }

    public override void OnStateUpdate()
    {
        AttackTimer();
        //������ �� �÷��̾� ����
        if(_timer > _slime._attackDelay)
        {
            _timer = 0f;
            //���⿡ ���ʹ� ���� �ֱ�
        }
    }
    public void AttackTimer()
    {
        _timer += Time.deltaTime;
    }
}
