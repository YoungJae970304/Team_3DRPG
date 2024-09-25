using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrkAttackState : MonsterBaseState
{
    public OrkAttackState(Ork ork) : base(ork) 
    {
        _ork = ork;
    }
    float _timer = 0f;
    public override void OnStateEnter()
    {
        
    }

    public override void OnStateExit()
    {
       
    }

    public override void OnStateUpdate()
    {
        AttackTimer();
        //딜레이 후 플레이어 공격
        if (_timer > _ork._attackDelay)
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
