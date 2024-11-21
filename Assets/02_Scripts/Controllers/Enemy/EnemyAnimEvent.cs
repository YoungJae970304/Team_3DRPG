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
        _bossBear = _monster as BossBear; // 다운캐스팅을하여 보스몬스터를 불러와서 담았습니다.
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
    public void Roar() // 보스몬스터의 포효와 관련된 함수입니다. 해당 함수로 플레이어와의 거리를 판단하여 포효 시 플레이어가 스턴되도록 처리하였습니다.
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
    public void AttackCompleteCheck() // 공격의 시작 여부를 판단하기 위하여 만든 함수입니다. 공격의 시작에 사용됩니다.
    {
        _monster._attackCompleted = false;
    }
    public void NomalAttack() //몬스터의 일반 공격에 사용되는 이벤트입니다. 평범하게 데미지를 입히기 위하여 사용됩니다.
    {

        Logger.Log("NomalAttack");

        _player._playerHitState = PlayerHitState.NomalAttack;
        _monster.AttackPlayer();

    }
    public void SwitchAttackEffect(int attack) // 몬스터의 이팩트를 실행하기 위하여 사용되는 함수입니다. 이팩트에 int값에 들어간 숫자인 0,1,2에 따라 이팩트가 다르게 실행됩니다.
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
    public void SkillAttack() // 몬스터의 스킬 공격에 사용되는 이벤트입니다. 해당 공격에 플레이어가 적중 시 일정시간 경직에 걸리게 됩니다.
    {

        Logger.Log("SkillAttack");

        _player._playerHitState = PlayerHitState.SkillAttack;
        _monster.AttackPlayer();

    }
    public void MonsterAnimFalse() // 몬스터의 애니메이션을 사용종료하기위한 함수입니다. 몬스터가 죽었을 때 호출됩니다.
    {
        _monster._anim.enabled = false;
    }
    public void AfterDamagedState() // 데미지판정후 사용되는 함수입니다. 판정 시 hp상태에 따라 현재 상태가 변경됩니다.
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
    public void MonsterAttackCheck() // 공격완료 시 실행되는 함수입니다. 공격완료 체크 후 다음상태로 넘어가기 위해 사용되었습니다.
    {
        //Logger.LogError("실행중bool124");
        _monster._attackCompleted = true;
        //Logger.LogError("실행중bool");
    }
}
