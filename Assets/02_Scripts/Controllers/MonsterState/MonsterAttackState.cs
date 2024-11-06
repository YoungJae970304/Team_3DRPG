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
        _monster._nav.destination = _monster.transform.position;
        _monster._nav.isStopped = true;
        _monster._attackCompleted = false;
        _monster._timer = 3f;
        _monster._anim.SetTrigger("BeforeAttack");
        _monster._anim.SetBool("BeforeChase", false);

    }

    public override void OnStateExit()
    {
        _monster._nav.isStopped = false;
        _monster._timer = _monster._mStat.AtkDelay;
        _monster._anim.SetBool("BeforeChase", true);
    }
    private IEnumerator AttackDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        _monster._anim.SetBool("AfterAttackMotion", true);
    }
    public override void OnStateUpdate()
    {
        _monster.AttackTimer();
        _monster._randomAttack = UnityEngine.Random.Range(1, 101);
        if (_monster._timer > _monster._mStat.AtkDelay)
        {
            _monster._anim.SetBool("AfterAttackMotion", false);
            _monster.AttackStateSwitch();
            _monster.StartCoroutine(AttackDelay(0.8f));
            _monster._timer = 0f;
        }
    }
}
