using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblemIdleState : MonsterBaseState
{
    public GoblemIdleState(Goblem goblem) : base(goblem)
    {
        _goblem = goblem;
    }
    GoblemStat _gStat;
    float awayRangeX;
    //float awayRangeY = Random.Range(0, _sStat.AwayRange);
    float awayRangeZ;
    public override void OnStateEnter()
    {
        _gStat = _goblem.GetComponent<GoblemStat>();
        if (_gStat == null)
        {
            Debug.LogError("SlimeStat ������Ʈ�� ã�� �� �����ϴ�.");
        }
        awayRangeX = Random.Range(-_gStat.AwayRange, _gStat.AwayRange);
        //float awayRangeY = Random.Range(0, _sStat.AwayRange);
        awayRangeZ = Random.Range(-_gStat.AwayRange, _gStat.AwayRange);
        _goblem._nav.destination = _goblem._originPos + new Vector3(awayRangeX, 0, awayRangeZ);
    }

    public override void OnStateExit()
    {
        //�긦 ��� �ؾ��ұ�
    }

    public override void OnStateUpdate()
    {
        if (_gStat == null) return;
        //���� �Ÿ� ��ȸ
        //���������� �÷��̾ ���� �Ÿ� �ȿ� ���´ٸ� Exit�� ���� ��ȯ
        awayRangeX = Random.Range(-_gStat.AwayRange, _gStat.AwayRange);
        //float awayRangeY = Random.Range(0, _sStat.AwayRange);
        awayRangeZ = Random.Range(-_gStat.AwayRange, _gStat.AwayRange);

        if ((_goblem._originPos + _goblem.transform.position).magnitude < (_goblem._originPos).magnitude + _gStat.ReturnRange ||
            (_goblem._originPos - _goblem.transform.position).magnitude > (_goblem._originPos).magnitude - _gStat.ReturnRange)
        {
            if ((_goblem._nav.destination - _goblem.transform.position).magnitude > 1f)
            {
                _goblem._nav.SetDestination(_goblem._nav.destination);
            }
            else if (_goblem._curState == Goblem.State.Move)
            {
                _goblem._nav.destination = _goblem._player.transform.position;
            }
            else
            {
                _goblem._nav.destination = _goblem._originPos + new Vector3(awayRangeX, 0, awayRangeZ);
            }
        }
        else
        {
            //������ �������� ���� ���� �̻����� ����ٸ� return�ϱ� - �ٵ� �� slime���� ����Ǿ���
            _goblem._nav.destination = _goblem._originPos;
        }
    }
}
