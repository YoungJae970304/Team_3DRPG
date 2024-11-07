using SkillModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaBurst : SkillBase
{
    //private const int SKILL_ID = 10;

    public ManaBurst(int skillId) : base(skillId)
    {
        Enter = new ManaBurstEnter();
        Stay = new ManaBurstStay();
        Exit = new ManaBurstExit();
        Passive = new NoneSkillPassive();
    }
}
public class ManaBurstEnter : SkillEnter
{
    public void Enter(ITotalStat stat, SkillData skillData, int level = 0)
    {
        Managers.Game._player._playerAnim.Play("Skill1");
    }
}

public class ManaBurstStay : SkillStay
{
    Animator _anim = Managers.Game._player._playerAnim;
    bool _damageApply = false;

    public void Stay(ITotalStat stat, SkillData skillData, int level = 0)
    {
        // 애니메이션 진행도 8&에서 30% 시점까지는 빠른 이동
        if (_anim.GetCurrentAnimatorStateInfo(0).IsName("Skill1"))
        {
            float normalizedTime = _anim.GetCurrentAnimatorStateInfo(0).normalizedTime % 1;

            // 8% 진행 지점에서 이벤트 트리거
            if (normalizedTime >= 0.44f && normalizedTime <= 0.46f && !_damageApply)
            {
                //플레이어 공격력 * ( (baseValue + (SkillLevel * DamageValue)) * 0.01 )
                //int damage = (int)(stat.ATK * ((skillData.BaseDamage + (level * skillData.DamageValue)) * 0.01f));
                Logger.Log($"플레이어 공격력 : {stat.ATK} / 스킬 BaseDamage : {skillData.BaseDamage} / 현재 스킬 레벨 : {level} / 스킬데미지값 : {skillData.DamageValue}");
                //Logger.LogError($"데미지 총합 : {damage}");
                Managers.Game._player.AreaDamage(15f, Managers.Game._player._skillBase._damage);

                _damageApply = true;
            }
        }
    }

    public void End(ITotalStat stat, SkillData skillData, int level = 0)
    {
        _damageApply = false;
    }
}

public class ManaBurstExit : SkillExit
{
    public void Exit(ITotalStat stat, SkillData skillData, int level = 0)
    {
        // 증가된 공격력 복구
    }
}
