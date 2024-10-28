using SkillModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSkill1 : SkillBase
{
    public override SkillEnter Enter { get; set; } = new MeleeSkill1Enter();
    public override SkillStay Stay { get; set; } = new MeleeSkill1Stay();
    public override SkillExit Exit { get; set; } = new MeleeSkill1Exit();
    public override SkillPassive Passive { get; set; } = new MeleeSkill1Passive();
}

public class MeleeSkill1Enter : SkillEnter
{
    public void Enter(ITotalStat stat, int level = 0)
    {
        Managers.Game._player.SetColActive("Skill1");

        Managers.Game._player._playerAnim.Play("Skill1");

        stat.MoveSpeed = 10;
    }
}

public class MeleeSkill1Stay : SkillStay
{
    Animator _anim = Managers.Game._player._playerAnim;
    bool _damageApply = false;

    public void Stay(ITotalStat stat, int level = 0)
    {
        // 애니메이션 진행도 8&에서 30% 시점까지는 빠른 이동
        if (_anim.GetCurrentAnimatorStateInfo(0).IsName("Skill1"))
        {
            float normalizedTime = _anim.GetCurrentAnimatorStateInfo(0).normalizedTime % 1;

            // 8% 진행 지점에서 이벤트 트리거
            if (normalizedTime >= 0.08f && normalizedTime <= 0.3f)
            {
                Managers.Game._player._cc.Move(Managers.Game._player._playerModel.forward * Managers.Game._player._playerStatManager.MoveSpeed * Time.deltaTime);
            }
            if (normalizedTime >= 0.4f && normalizedTime <= 0.42f && !_damageApply)
            {
                Managers.Game._player.ApplyDamage(stat.ATK);
                _damageApply = true;
            }
        }
    }

    public void End(ITotalStat stat, int level = 0)
    {
        _damageApply = false;
    }
}

public class MeleeSkill1Exit : SkillExit
{
    public void Exit(ITotalStat stat, int level = 0)
    {
        Managers.Game._player.SetColActive("Katana");

        // 증가된 속도 복구
        stat.MoveSpeed = -10;
    }
}

public class MeleeSkill1Passive : SkillPassive
{
    public void Passive(ITotalStat stat, int level = 0)
    {
        stat.MaxHP = 50;
    }
}
