using SkillModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSkill2 : SkillBase
{
    public override SkillEnter Enter { get; set; } = new MeleeSkill2Enter();
    public override SkillStay Stay { get; set; } = new MeleeSkill2Stay();
    public override SkillExit Exit { get; set; } = new MeleeSkill2Exit();
    public override SkillPassive Passive { get; set; } = new MeleeSkill2Passive();
}

public class MeleeSkill2Enter : SkillEnter
{
    public void Enter(ITotalStat stat, int level = 0)
    {
        Managers.Game._player._playerAnim.Play("Skill2");

        stat.ATK = 10;
    }
}

public class MeleeSkill2Stay : SkillStay
{
    Animator _anim = Managers.Game._player._playerAnim;
    bool _damageApply = false;

    public void Stay(ITotalStat stat, int level = 0)
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

    public void End(ITotalStat stat, int level = 0)
    {
        _damageApply = false;
    }
}

public class MeleeSkill2Exit : SkillExit
{
    public void Exit(ITotalStat stat, int level = 0)
    {
        Managers.Game._player.SetColActive("Katana");

        // 복구
        stat.ATK = -10;
    }
}

public class MeleeSkill2Passive : SkillPassive
{
    public void Passive(ITotalStat stat, int level = 0)
    {
        Debug.Log("TestSkill 패시브 효과");

        stat.MaxHP = 50;
    }
}
