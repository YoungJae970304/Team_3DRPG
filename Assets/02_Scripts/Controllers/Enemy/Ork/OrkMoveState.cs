using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrkMoveState : MonsterBaseState
{
    public OrkMoveState(Ork ork) : base(ork) 
    {
        _ork = ork;
        _oStat = _ork._oStat;
    }
    float _timer = 0;
    OrkStat _oStat;
    public override void OnStateEnter()
    {
        //�÷��̾� ã��(�����ӿ��� ã�Ƶ�)
        _ork._nav.stoppingDistance = _oStat.AttackRange;
        _ork._nav.destination = _ork._player.transform.position;
    }

    public override void OnStateExit()
    {
        _ork._nav.stoppingDistance = 0;
    }

    public override void OnStateUpdate()
    {
        //�÷��̾� �߰�
        _ork._nav.SetDestination(_ork._nav.destination);
        _timer += Time.deltaTime;
        if (_timer > 2f)
        {
            _ork._nav.destination = _ork._player.transform.position;
        }
    }
}
