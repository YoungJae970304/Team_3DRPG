using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BossBearAttackState : BaseState
{
    public BossBearAttackState(Player player, Monster monster, Stat stat) : base(player, monster, stat) { }

    float _timer;
    int _randomAttack;

    public override void OnStateEnter()
    {
        _timer = 0;
        _monster._nav.stoppingDistance = _monster._bStat.AttackRange;
    }

    public override void OnStateExit()
    {
        _timer = _monster._attackDelay;
        _monster._nav.stoppingDistance = 0;
    }

    public override void OnStateUpdate()
    {
        AttackTimer();
        
        _randomAttack = UnityEngine.Random.Range(1, 101);
        //딜레이 후 플레이어 공격
        if (_timer > _bossBear._attackDelay)
        {
            _timer = 0f;
            //여기에 에너미 공격 넣기
            
            AttackStateSwitch();
            Logger.Log(11.ToString());
        }
    }
    public void AttackTimer()
    {
        _timer += Time.deltaTime;
    }
    public void AttackPlayer() // 일단 여기에 넣어놨는데 애니메이션에서 호출하는 이벤트방식으로 쓸듯
    {
        _player.Damaged(_bStat.Attack);
        
    }
    public void AttackStateSwitch()
    {
        
        if(_randomAttack <= 15)
        {
            LeftBiteAttack();
        }
        else if(_randomAttack <= 30)
        {
            RightBiteAttack();
        }
        else if(_randomAttack <= 60)
        {
            LeftHandAttack();
        }
        else if (_randomAttack <= 90)
        {
            RightHandAttack();
        }
        else 
        {
            EarthquakeAttack();
        }
    }
    public void EarthquakeAttack()
    {
        Logger.Log("EarthquakeAttack");
        AttackPlayer();
    }
    public void LeftBiteAttack()
    {
        Logger.Log("LeftBiteAttack");
        AttackPlayer();
    }
    public void RightBiteAttack()
    {
        Logger.Log("RightBiteAttack");
        AttackPlayer();
    }
    public void LeftHandAttack()
    {
        Logger.Log("LeftHandAttack");
        AttackPlayer();
    }
    public void RightHandAttack()
    {
        Logger.Log("RightHandAttack");
        AttackPlayer();
    }
}
