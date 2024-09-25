using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrkIdleState : MonsterBaseState
{
    public OrkIdleState(Ork ork) : base(ork)
    {
        _ork = ork;
    }
    OrkStat _oStat;
    float awayRangeX;
    //float awayRangeY = Random.Range(0, _sStat.AwayRange);
    float awayRangeZ;
    public override void OnStateEnter()
    {
        _oStat = _ork.GetComponent<OrkStat>();
        if (_oStat == null)
        {
            Debug.LogError("SlimeStat ������Ʈ�� ã�� �� �����ϴ�.");
        }
        awayRangeX = Random.Range(-_oStat.AwayRange, _oStat.AwayRange);
        //float awayRangeY = Random.Range(0, _sStat.AwayRange);
        awayRangeZ = Random.Range(-_oStat.AwayRange, _oStat.AwayRange);
        _ork._nav.destination = _ork._originPos + new Vector3(awayRangeX, 0, awayRangeZ);
    }

    public override void OnStateExit()
    {
        //�긦 ��� �ؾ��ұ�
    }

    public override void OnStateUpdate()
    {
        if (_oStat == null) return;
        //���� �Ÿ� ��ȸ
        //���������� �÷��̾ ���� �Ÿ� �ȿ� ���´ٸ� Exit�� ���� ��ȯ
        awayRangeX = Random.Range(-_oStat.AwayRange, _oStat.AwayRange);
        //float awayRangeY = Random.Range(0, _sStat.AwayRange);
        awayRangeZ = Random.Range(-_oStat.AwayRange, _oStat.AwayRange);

        if ((_ork._originPos + _ork.transform.position).magnitude < (_ork._originPos).magnitude + _oStat.ReturnRange ||
            (_ork._originPos - _ork.transform.position).magnitude > (_ork._originPos).magnitude - _oStat.ReturnRange)
        {
            if ((_ork._nav.destination - _ork.transform.position).magnitude > 1f)
            {
                _ork._nav.SetDestination(_ork._nav.destination);
            }
            else if (_ork._curState == Ork.State.Move)
            {
                _ork._nav.destination = _ork._player.transform.position;
            }
            else
            {
                _ork._nav.destination = _ork._originPos + new Vector3(awayRangeX, 0, awayRangeZ);
            }
        }
        else
        {
            //������ �������� ���� ���� �̻����� ����ٸ� return�ϱ� - �ٵ� �� slime���� ����Ǿ���
            _ork._nav.destination = _ork._originPos;
        }
    }
}
