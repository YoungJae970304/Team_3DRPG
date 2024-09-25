using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeDieState : MonsterBaseState
{
    public SlimeDieState(Slime slime) : base(slime) { }

    public override void OnStateEnter()
    {
        //�״� ���
        _slime.DropItem();
        OnStateExit();
    }

    public override void OnStateExit()
    {
        //exit�� �ʿ��ұ�
        _slime.SlimeDie();
    }

    public override void OnStateUpdate()
    {    
        //����Ʈ ��ġ �����ϱ�
    }
}
