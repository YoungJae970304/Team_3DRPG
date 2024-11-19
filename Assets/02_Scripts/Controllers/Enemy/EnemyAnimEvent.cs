using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimEvent : MonoBehaviour
{
    Monster _monster;
    Player _player;
    BossBear _bossBear;
    void Start()
    {
        _monster = GetComponentInParent<Monster>();
        _player = Managers.Game._player;
        _bossBear = _monster as BossBear;
        //Logger.LogError($"{_bossBear._maxRoarRange.transform.localScale.x * 2}");
        //Logger.LogError($"{Physics.OverlapSphere(transform.position, _bossBear._maxRoarRange.transform.localScale.x * 2).Length}범위");
    }
    #region 보스 사운드
    public void BossAtkSound() 
    {
        Managers.Sound.Play("Enemy/boss_atk");
    }
    public void BossHitSound() 
    {
        Managers.Sound.Play("Enemy/boss_atk_hit");
    }
    public void BossDie()
    {
        Managers.Sound.Play("Enemy/boss_die");
    }
    public void BossDamage()
    {
        Managers.Sound.Play("Enemy/boss_dmg");
    }
    public void BossRoar()
    {
        Managers.Sound.Play("Enemy/boss_roar");
    }
    public void BossRoarHit()
    {
        Managers.Sound.Play("Enemy/boss_roar_hit");
    }
    #endregion
    #region 고블린 사운드
    public void GoblinAtk()
    {
        Managers.Sound.Play("Enemy/goblin_atk");
    }
    public void GoblinHitAtk()
    {
        Managers.Sound.Play("Enemy/goblin_atk_hit");
    }
    public void GoblinDie()
    {
        Managers.Sound.Play("Enemy/goblin_die"); 
    }
    public void GoblinDmg()
    {
        Managers.Sound.RandSoundsPlay("Enemy/goblin_dmg_1", "Enemy/goblin_dmg_2");
    }
    #endregion
    #region 오크 사운드
    public void OrkAtk()
    {
        Managers.Sound.Play("Enemy/ork_atk");
    }
    public void OrkHitAttack()
    {
        Managers.Sound.Play("Enemy/goblin_atk_hit");
    }
    public void OrkDie()
    {
        Managers.Sound.Play("Enemy/ork_die"); 
    }
    public void OrkDmg()
    {
        Managers.Sound.RandSoundsPlay("Enemy/ork_dmg_1", "Enemy/ork_dmg_2");
    }
    #endregion
    #region 슬라임 사운드
    public void SlimeAtk()
    {
        Managers.Sound.Play("Enemy/slime_atk");
    }
    public void SlimeHitAtk()
    {
        Managers.Sound.Play("Enemy/slime_atk_hit");
    }
    public void SlimeDie()
    {
        Managers.Sound.Play("Enemy/slime_die");
    }
    public void SlimeDmg()
    {
        Managers.Sound.Play("Enemy/slime_dmg_1");
    }
    #endregion
    public void Roar()
    {
        //int damage = 0;
        
        Collider[] checkColliders = Physics.OverlapSphere(transform.position, _bossBear._maxRoarRange.transform.position.x);
        foreach (Collider collider in checkColliders)
        {
            if (collider.CompareTag("Player"))
            {
                if((collider.bounds.center - _monster._collider.bounds.center).magnitude + 4.5f < _bossBear._maxRoarRange.GetComponent<SpriteRenderer>().size.x* _bossBear._maxRoarRange.transform.localScale.x)
                {
                    if (collider.TryGetComponent<IDamageAlbe>(out var damageable))
                    {
                        damageable.StatusEffect.SpawnEffect<StunEffect>(1);
                        BossRoarHit();
                        //_player.Damaged(_mStat.ATK);
                    }
                }
            }
        }
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
                _monster._enemyEffect.MonsterAttack(EnemyEffect.GoblemOrkEffects.LeftAttack);
                break;
            case 1:
                _monster._enemyEffect.MonsterAttack(EnemyEffect.GoblemOrkEffects.RightAttack);
                break;
            case 2:
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
        //Logger.LogError("실행중bool124");
        _monster._attackCompleted = true;
        //Logger.LogError("실행중bool");
    }
}
