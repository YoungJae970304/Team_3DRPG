using SkillModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawSword : SkillBase
{
    private const int SKILL_ID = 2;

    public DrawSword() : base(SKILL_ID)
    {
        Enter = new DrawSwordEnter();
        Stay = new DrawSwordStay();
        Exit = new DrawSwordExit();
        Passive = new NoneSkillPassive();
    }
}

public class DrawSwordEnter : SkillEnter
{
    public void Enter(ITotalStat stat, SkillData skillData, int level = 0)
    {
        Managers.Game._player._playerAnim.Play("Skill2");
    }
}

public class DrawSwordStay : SkillStay
{
    Animator _anim = Managers.Game._player._playerAnim;
    bool _damageApply = false;

    public void Stay(ITotalStat stat, SkillData skillData, int level = 0)
    {
        // 애니메이션 진행도 8&에서 30% 시점까지는 빠른 이동
        if (_anim.GetCurrentAnimatorStateInfo(0).IsName("Skill2"))
        {
            float normalizedTime = _anim.GetCurrentAnimatorStateInfo(0).normalizedTime % 1;

            // 8% 진행 지점에서 이벤트 트리거
            if (normalizedTime >= 0.25f && normalizedTime <= 0.27f && !_damageApply)
            {
                Managers.Game._player.AreaDamage(15f, stat.ATK);
                _damageApply = true;
            }
        }
    }

    public void End(ITotalStat stat, SkillData skillData, int level = 0)
    {
        _damageApply = false;
    }
}

public class DrawSwordExit : SkillExit
{
    public void Exit(ITotalStat stat, SkillData skillData, int level = 0)
    {
        Managers.Game._player.SetColActive("Katana");
    }
}
