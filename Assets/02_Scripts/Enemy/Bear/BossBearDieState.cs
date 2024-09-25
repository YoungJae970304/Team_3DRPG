using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBearDieState : MonsterBaseState
{
    public BossBearDieState(BossBear bossBear) : base(bossBear)
    {
        _bossBear = bossBear;
    }

    public override void OnStateEnter()
    {
        //�״� ���
        _bossBear.DropItem();
        OnStateExit();
    }

    public override void OnStateExit()
    {
        //exit�� �ʿ��ұ�
        _bossBear.BossBearDie();
    }

    public override void OnStateUpdate()
    {
        //����Ʈ ��ġ �����ϱ�
    }
}
