using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMoveState : MonsterBaseState
{
    public SlimeMoveState(Slime slime) : base(slime) { }
    float _timer = 0;
    public override void OnStateEnter()
    {
        //�÷��̾� ã��(�����ӿ��� ã�Ƶ�)
        _slime._nav.ResetPath();
        _slime._nav.stoppingDistance = _slime._sStat.AttackRange;
        _slime._nav.destination = _slime._player.transform.position;
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
        //�굵 �������̶� ���ؼ� ���� Ȯ���ϱ�
    }

    public override void OnStateUpdate()
    {
        //�÷��̾� �߰�
        _slime._nav.SetDestination(_slime._nav.destination);
        _timer += Time.deltaTime;
        if(_timer > 2f)
        {
            _slime._nav.destination = _slime._player.transform.position;
        }
    }

}
