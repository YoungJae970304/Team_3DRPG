using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackState : BaseState
{
    public MonsterAttackState(Player player, Monster monster, Stat stat) : base(player, monster, stat)
    {
    }

   

    public override void OnStateEnter()
    {
        _monster.LookPlayer();
        _monster._anim.SetBool("BeforeChase", false);
        //_monster._hitPlayer.Clear();
        _monster._timer = 0;
        _monster._nav.stoppingDistance = _monster._mStat.AttackRange;
        _monster._anim.SetTrigger("BeforeAttack");
    }

    public override void OnStateExit()
    {
        _monster._timer = _monster._mStat.AtkDelay;
        _monster._nav.stoppingDistance = 0;
        
        _monster._anim.SetTrigger("AfterAttack");
        _monster._anim.SetBool("BeforeChase", true);
        //_monster._hitPlayer.Clear();
    }

    public override void OnStateUpdate()
    {
        _monster.AttackTimer();
        
        _monster._randomAttack = UnityEngine.Random.Range(1, 101);
        //딜레이 후 플레이어 공격
        if (_monster._timer > _monster._mStat.AtkDelay)
        {
            _monster.AttackStateSwitch();
            _monster._timer = 0f;
            //여기에 에너미 공격 넣기

            

        }
     
    }
}
