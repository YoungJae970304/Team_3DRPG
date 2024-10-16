using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackState : BaseState
{
    public MonsterAttackState(Player player, Monster monster, ITotalStat stat) : base(player, monster, stat)
    {
    }

   

    public override void OnStateEnter()
    {
        
        _monster._nav.isStopped = true;
        //_monster._nav.ResetPath();
        //초기화
        _monster._attackCompleted = false;
        //_monster.LookBeforeAttack();
        _monster._anim.SetBool("BeforeChase", false);
        //_monster._hitPlayer.Clear();
        _monster._timer = 3f;
        
        //_monster._nav.stoppingDistance = _monster._mStat.AttackRange-0.5f;
        _monster._anim.SetTrigger("BeforeAttack");
    }

    public override void OnStateExit()
    {
        //_monster._nav.enabled = true;
        _monster._nav.isStopped = false;
        _monster._timer = _monster._mStat.AtkDelay;
        _monster._nav.stoppingDistance = 0;
        
       
        _monster._anim.SetBool("BeforeChase", true);
        //_monster._hitPlayer.Clear();
    }
    private IEnumerator AttackDelay(float delay)
    {
        
        //_monster._nav.enabled = false;
        yield return new WaitForSeconds(delay);
        _monster._anim.SetBool("AfterAttackMotion", true);

        yield return new WaitForSeconds(1.2f); 
        _monster._attackCompleted = true;
        //_monster._nav.enabled = true;
    }
    public override void OnStateUpdate()
    {
        
        _monster.AttackTimer();
        //_monster.LookPlayer();
        
        _monster._randomAttack = UnityEngine.Random.Range(1, 101);

        //딜레이 후 플레이어 공격
        if (_monster._timer > _monster._mStat.AtkDelay)
        {
            //_monster._attackCompleted = false;
            _monster.LookPlayer();
            _monster._anim.SetBool("AfterAttackMotion", false);
            _monster.AttackStateSwitch();
            _monster.StartCoroutine(AttackDelay(0.8f));
            _monster._timer = 0f;
           
            //여기에 에너미 공격 넣기
        }
    }
}
