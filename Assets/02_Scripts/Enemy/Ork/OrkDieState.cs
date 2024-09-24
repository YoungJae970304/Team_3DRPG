using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrkDieState : MonsterBaseState
{
    public OrkDieState(Ork ork) : base(ork) { }

    public override void OnStateEnter()
    {
        //�״� ���
        _ork.DropItem();
        OnStateExit();
    }

    public override void OnStateExit()
    {
        //exit�� �ʿ��ұ�
        _ork.OrkDie();
    }

    public override void OnStateUpdate()
    {
        //����Ʈ ��ġ �����ϱ�
    }

}
