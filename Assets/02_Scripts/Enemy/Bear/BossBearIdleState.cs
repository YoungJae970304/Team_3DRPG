using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBearIdleState : MonsterBaseState
{
    public BossBearIdleState(BossBear bossBear) : base(bossBear)
    {
        _bossBear = bossBear;
    }
    BearStat _bStat;
    float awayRangeX;
    //float awayRangeY = Random.Range(0, _sStat.AwayRange);
    float awayRangeZ;
    public override void OnStateEnter()
    {
        _bStat = _bossBear.GetComponent<BearStat>();
        if (_bStat == null)
        {
            Debug.LogError("SlimeStat ������Ʈ�� ã�� �� �����ϴ�.");
        }
        awayRangeX = Random.Range(-_bStat.AwayRange, _bStat.AwayRange);
        //float awayRangeY = Random.Range(0, _sStat.AwayRange);
        awayRangeZ = Random.Range(-_bStat.AwayRange, _bStat.AwayRange);
        _bossBear._nav.destination = _bossBear._originPos + new Vector3(awayRangeX, 0, awayRangeZ);
    }

    public override void OnStateExit()
    {
        //�긦 ��� �ؾ��ұ�
    }

    public override void OnStateUpdate()
    {
        if (_bStat == null) return;
        //���� �Ÿ� ��ȸ
        //���������� �÷��̾ ���� �Ÿ� �ȿ� ���´ٸ� Exit�� ���� ��ȯ
        awayRangeX = Random.Range(-_bStat.AwayRange, _bStat.AwayRange);
        //float awayRangeY = Random.Range(0, _sStat.AwayRange);
        awayRangeZ = Random.Range(-_bStat.AwayRange, _bStat.AwayRange);

        if ((_bossBear._originPos + _bossBear.transform.position).magnitude < (_bossBear._originPos).magnitude + _bStat.ReturnRange ||
            (_bossBear._originPos - _bossBear.transform.position).magnitude > (_bossBear._originPos).magnitude - _bStat.ReturnRange)
        {
            if ((_bossBear._nav.destination - _bossBear.transform.position).magnitude > 1f)
            {
                _bossBear._nav.SetDestination(_bossBear._nav.destination);
            }
            else if (_bossBear._curState == BossBear.State.Move)
            {
                _bossBear._nav.destination = _bossBear._player.transform.position;
            }
            else
            {
                _bossBear._nav.destination = _bossBear._originPos + new Vector3(awayRangeX, 0, awayRangeZ);
            }
        }
        else
        {
            //������ �������� ���� ���� �̻����� ����ٸ� return�ϱ� - �ٵ� �� slime���� ����Ǿ���
            _bossBear._nav.destination = _bossBear._originPos;
        }
    }
}
