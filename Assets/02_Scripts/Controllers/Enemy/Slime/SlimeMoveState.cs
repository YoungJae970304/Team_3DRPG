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
        //�÷��̾� ã��(�����ӿ��� ã�Ƶ�)
        _slime._nav.stoppingDistance = _sStat.AttackRange;
        _slime._nav.destination = _slime._player.transform.position;
    }

    public override void OnStateExit()
    {
        _slime._nav.stoppingDistance = 0;
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
