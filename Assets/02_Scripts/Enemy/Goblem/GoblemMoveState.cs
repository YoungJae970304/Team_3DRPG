using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblemMoveState : MonsterBaseState
{
    public GoblemMoveState(Goblem goblem) : base(goblem)
    {
        _goblem = goblem;
        _gStat = _goblem._gStat;
    }
    float _timer = 0;
    GoblemStat _gStat;
    public override void OnStateEnter()
    {
        //�÷��̾� ã��(�����ӿ��� ã�Ƶ�)
        _goblem._nav.stoppingDistance = _gStat.AttackRange;
        _goblem._nav.destination = _goblem._player.transform.position;
    }

    public override void OnStateExit()
    {

    }

    public override void OnStateUpdate()
    {
        //�÷��̾� �߰�
        _goblem._nav.SetDestination(_goblem._nav.destination);
        _timer += Time.deltaTime;
        if (_timer > 2f)
        {
            _goblem._nav.destination = _goblem._player.transform.position;
        }
    }
}

