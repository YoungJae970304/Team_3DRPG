using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMoveState : MonsterBaseState
{
    public SlimeMoveState(Slime slime) : base(slime) { }

    public override void OnStateEnter()
    {
        //�÷��̾� ã��(�����ӿ��� ã�Ƶ�)
        _slime._nav.ResetPath();
        _slime._nav.stoppingDistance = _slime._mStat.AttackRange;
        _slime._nav.destination = _slime._player.transform.position;
        OnStateUpdate();
    }

    public override void OnStateExit()
    {
        //�ٸ� ���·� ��ȯ
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
        //�÷��̾� �߰�
        _slime._nav.Move(_slime._player.transform.position);
        OnStateExit();
    }

}
