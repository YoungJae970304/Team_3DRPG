using SkillModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSkill3 : SkillBase
{
    public override SkillEnter Enter { get; set; } = new MeleeSkill3Enter();
    public override SkillStay Stay { get; set; } = new MeleeSkill3Stay();
    public override SkillExit Exit { get; set; } = new MeleeSkill3Exit();
    public override SkillPassive Passive { get; set; } = new MeleeSkill3Passive();
}

public class MeleeSkill3Enter : SkillEnter
{
    public void Enter(ITotalStat stat, int level = 0)
    {
        Managers.Game._player._playerAnim.Play("Skill3");

        stat.ATK = 10;
    }
}

public class MeleeSkill3Stay : SkillStay
{
    Animator _anim = Managers.Game._player._playerAnim;

    public void Stay(ITotalStat stat, int level = 0)
    {

    }

    public void End(ITotalStat stat, int level = 0)
    {

    }
}

public class MeleeSkill3Exit : SkillExit
{
    public void Exit(ITotalStat stat, int level = 0)
    {
        Managers.Game._player.SetColActive("Katana");

        stat.ATK = -10;
    }
}

public class MeleeSkill3Passive : SkillPassive
{
    public void Passive(ITotalStat stat, int level = 0)
    {
        Debug.Log("TestSkill 패시브 효과");

        stat.MaxHP = 50;
    }
}
