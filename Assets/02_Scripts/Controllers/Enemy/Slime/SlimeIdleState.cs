using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class SlimeIdleState : MonsterBaseState
{
    public SlimeIdleState(Slime slime) : base(slime) 
    {
        _slime = slime;
    }
    SlimeStat _sStat;
    float awayRangeX;
    //float awayRangeY = Random.Range(0, _sStat.AwayRange);
    float awayRangeZ;
    public override void OnStateEnter()
    {
        //���� ��ġ ����
        //�ٵ� �̰͵� �����ӿ��� ���ݾ�
        _sStat = _slime.GetComponent<SlimeStat>();
        if (_sStat == null)
        {
            Debug.LogError("SlimeStat ������Ʈ�� ã�� �� �����ϴ�.");
        }
        awayRangeX = Random.Range(-_sStat.AwayRange, _sStat.AwayRange);
        //float awayRangeY = Random.Range(0, _sStat.AwayRange);
        awayRangeZ = Random.Range(-_sStat.AwayRange, _sStat.AwayRange);
        _slime._nav.destination = _slime._originPos + new Vector3(awayRangeX, 0, awayRangeZ);
    }

    public override void OnStateExit()
    {
        //�긦 ��� �ؾ��ұ�
    }

    public override void OnStateUpdate()
    {
        if (_sStat == null) return;
        //���� �Ÿ� ��ȸ
        //�������� ��� ��ȸ
        //���������� �÷��̾ ���� �Ÿ� �ȿ� ���´ٸ� Exit�� ���� ��ȯ
        awayRangeX = Random.Range(-_sStat.AwayRange, _sStat.AwayRange);
        //float awayRangeY = Random.Range(0, _sStat.AwayRange);
        awayRangeZ = Random.Range(-_sStat.AwayRange, _sStat.AwayRange);
        
        if ((_slime._originPos + _slime.transform.position).magnitude < (_slime._originPos).magnitude + _sStat.ReturnRange ||
            (_slime._originPos - _slime.transform.position).magnitude > (_slime._originPos).magnitude - _sStat.ReturnRange)
        {
            if((_slime._nav.destination - _slime.transform.position).magnitude > 1f)
            {
                _slime._nav.SetDestination(_slime._nav.destination);
            }
            else if((_slime._nav.destination - _slime.transform.position).magnitude <= 1f)
            {
                _slime._nav.destination = _slime._originPos + new Vector3(awayRangeX, 0, awayRangeZ);
            }
        }
        else
        {
            //������ �������� ���� ���� �̻����� ����ٸ� return�ϱ� - �ٵ� �� slime���� ����Ǿ���
            _slime._nav.destination = _slime._originPos;
        }
    }
}