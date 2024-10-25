using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimEvent : MonoBehaviour
{
    Monster _monster;
    Player _player;
    void Start()
    {
        _monster = GetComponentInParent<Monster>();
        _player = Managers.Game._player;
    }
    public void AttackCompleteCheck()
    {
        _monster._attackCompleted = false;
    }
    public void NomalAttack() //이벤트 2번
    {

        Logger.Log("NomalAttack");

        _player._playerHitState = PlayerHitState.NomalAttack;
        _monster.AttackPlayer();

    }
    public void SwitchAttackEffect(int attack)
    {
        switch (attack)
        {
            case 0:
                Logger.LogError("1번이펙트");
                _monster._enemyEffect.MonsterAttack(EnemyEffect.GoblemOrkEffects.LeftAttack);
                break;
            case 1:
                Logger.LogError("2번이펙트");
                _monster._enemyEffect.MonsterAttack(EnemyEffect.GoblemOrkEffects.RightAttack);
                break;
            case 2:
                Logger.LogError("3번 이펙트");
                _monster._enemyEffect.MonsterAttack(EnemyEffect.GoblemOrkEffects.Roar);
                break;
        }
    }
    public void SkillAttack() // 이벤트 2번
    {

        Logger.Log("SkillAttack");

        _player._playerHitState = PlayerHitState.SkillAttack;
        _monster.AttackPlayer();

    }
    public void MonsterAnimFalse() // 애니메이션 이벤트용
    {
        _monster._anim.enabled = false;
    }
    public void AfterDamagedState()
    {
        // HP 상태에 따른 상태 전환
        if (_monster._mStat.HP <= 0)
        {
            _monster.MChangeState(Monster.MonsterState.Die);

        }
        else
        {
            _monster.MChangeState(Monster.MonsterState.Move);
        }
    }
    public void MonsterAttackCheck()
    {
        _monster._attackCompleted = true;
    }
}
