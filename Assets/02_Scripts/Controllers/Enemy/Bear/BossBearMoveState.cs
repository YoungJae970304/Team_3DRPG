using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBearMoveState : MonsterBaseState
{
    public BossBearMoveState(BossBear bossBear) : base(bossBear)
    {
        _bossBear = bossBear;
        _bStat = _bossBear._bStat;
    }
    float _timer = 0;
    BearStat _bStat;
    public override void OnStateEnter()
    {
        //�÷��̾� ã��(�����ӿ��� ã�Ƶ�)
        _bossBear._nav.stoppingDistance = _bStat.AttackRange;
        _bossBear._nav.destination = _bossBear._player.transform.position;
    }

    public override void OnStateExit()
    {
        _bossBear._nav.stoppingDistance = 0f;
    }

    public override void OnStateUpdate()
    {
        //�÷��̾� �߰�
        _bossBear._nav.SetDestination(_bossBear._nav.destination);
        _timer += Time.deltaTime;
        if (_timer > 2f)
        {
            _bossBear._nav.destination = _bossBear._player.transform.position;
        }
    }
}
