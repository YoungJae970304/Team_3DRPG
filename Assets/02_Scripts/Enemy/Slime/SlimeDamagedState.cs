using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeDamagedState : MonsterBaseState
{
    public SlimeDamagedState(Slime slime) : base(slime) { }
    public override void OnStateEnter()
    {
        //�˹�, ������ �ޱ�
        //�ӽ÷� ������ �������� �־���� ���� �÷��̾� ������ �� �޾ƿ��� ����
        _slime.StartCoroutine(_slime.StartDamege(_monster._mStat.Attack, _slime.transform.position, 0.5f, 0.5f));
        OnStateUpdate();
    }

    public override void OnStateExit()
    {
        //�ٸ� ���·� ��ȯ
        
        
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
        //������Ʈ�� ���� �ֳ�?
        OnStateExit();
    }
    
}
