using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblemAttackState : MonsterBaseState
{
    float _timer;
    public GoblemAttackState(Goblem goblem) : base(goblem)
    {
        _goblem = goblem;
        _gStat = _goblem._gStat;
        _player = _goblem._player.GetComponent<Player>();
        _pStat = _player._playerStat;
    }
    PlayerStat _pStat;
    GoblemStat _gStat;
    int _randomAttack;
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
        _randomAttack = Random.Range(1, 99);
        //딜레이 후 플레이어 공격
        if (_timer > _goblem._attackDelay)
        {
            _timer = 0f;
            //여기에 에너미 공격 넣기
           
            AttackStateSwitch();
        }
    }
    public void AttackTimer()
    {
        _timer += Time.deltaTime;
    }
    public void AttackPlayer()
    {
        _player.Damaged(_gStat.Attack);
    }
    public void AttackStateSwitch()
    {

        if (_randomAttack <= 66)
        {
            NomalAttack();
        }
        else
        {
            SkillAttack();
        }
        
    }
    public void NomalAttack()
    {
        Logger.Log("NomalAttack");
        AttackPlayer();
    }
    public void SkillAttack()
    {
        Logger.Log("SkillAttack");
        AttackPlayer();
    }
    
}
