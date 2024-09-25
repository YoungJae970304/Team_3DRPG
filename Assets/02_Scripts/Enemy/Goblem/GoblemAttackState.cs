using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblemAttackState : MonsterBaseState
{
    float _timer;
    public GoblemAttackState(Goblem goblem) : base(goblem)
    {
        _goblem = goblem;
    }

    public override void OnStateEnter()
    {
        _timer = 0;
    }

    public override void OnStateExit()
    {
        _timer = _goblem._attackDelay;
    }

    public override void OnStateUpdate()
    {
        AttackTimer();
        //������ �� �÷��̾� ����
        if (_timer > _goblem._attackDelay)
        {
            _timer = 0f;
            //���⿡ ���ʹ� ���� �ֱ�
        }
    }
    public void AttackTimer()
    {
        _timer += Time.deltaTime;
    }
}
