using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class SlimeIdleState : MonsterBaseState
{
    public SlimeIdleState(Slime slime) : base(slime) { }
    Vector3 _originPos;
    public override void OnStateEnter()
    {
        //���� ��ġ ����
        _originPos = _monster.transform.position;
        OnStateUpdate();
    }

    public override void OnStateExit()
    {
        _slime.ChangeState(Slime.State.Damage);
    }

    public override void OnStateUpdate()
    {
        //���� �Ÿ� ��ȸ
        //�������� ��� ��ȸ
        //���������� �÷��̾ ���� �Ÿ� �ȿ� ���´ٸ� Exit�� ���� ��ȯ
        float awayRangeX = Random.Range(0, _monster._mStat.AwayRange + 1);
        float awayRangeY = Random.Range(0, _monster._mStat.AwayRange + 1);
        float awayRangeZ = Random.Range(0, _monster._mStat.AwayRange + 1);
        if ((_originPos + _monster.transform.position).magnitude < (_originPos).magnitude + _monster._mStat.ReturnRange)
        {
            _monster._nav.Move(_originPos + new Vector3(awayRangeX, awayRangeY, awayRangeZ));
        }
        else
        {
            OnStateExit();
        }
    }
}