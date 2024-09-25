using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblemDieState: MonsterBaseState
{
    public GoblemDieState(Goblem goblem) : base(goblem)
    {
        _goblem = goblem;
    }

    public override void OnStateEnter()
    {
        //�״� ���
        _goblem.DropItem();
        OnStateExit();
    }

    public override void OnStateExit()
    {
        //exit�� �ʿ��ұ�
        _goblem.GoblemDie();
    }

    public override void OnStateUpdate()
    {
        //����Ʈ ��ġ �����ϱ�
    }

}
