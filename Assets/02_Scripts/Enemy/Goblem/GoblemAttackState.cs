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
        //딜레이 후 플레이어 공격
        if (_timer > _goblem._attackDelay)
        {
            _timer = 0f;
            //여기에 에너미 공격 넣기
        }
    }
    public void AttackTimer()
    {
        _timer += Time.deltaTime;
    }
}
