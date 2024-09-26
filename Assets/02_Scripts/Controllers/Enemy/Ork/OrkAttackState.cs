using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrkAttackState : MonsterBaseState
{
    public OrkAttackState(Ork ork) : base(ork) 
    {
        _ork = ork;
        _oStat = _ork._oStat;
        _player = _ork._player.GetComponent<Player>();
        _pStat = _player._playerStat;
    }
    PlayerStat _pStat;
    OrkStat _oStat;
    float _timer = 0f;
    int _randomAttack;
    public override void OnStateEnter()
    {
        
    }

    public override void OnStateExit()
    {
       
    }

    public override void OnStateUpdate()
    {
        AttackTimer();
        _randomAttack = Random.Range(1, 99);
        //딜레이 후 플레이어 공격
        if (_timer > _ork._attackDelay)
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
        _player.Damaged(_oStat.Attack);
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
