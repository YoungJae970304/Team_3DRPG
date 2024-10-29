using SkillModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBurst : SkillBase
{
    private const int SKILL_ID = 3;

    public SwordBurst() : base(SKILL_ID)
    {
        Enter = new SwordBurstEnter();
        Stay = new SwordBurstStay();
        Exit = new SwordBurstExit();
        Passive = new NoneSkillPassive();
    }
}

public class SwordBurstEnter : SkillEnter
{
    public void Enter(ITotalStat stat, SkillData skillData, int level = 0)
    {
        Managers.Game._player._playerAnim.Play("Skill3");
    }
}

public class SwordBurstStay : SkillStay
{
    Animator _anim = Managers.Game._player._playerAnim;

    public void Stay(ITotalStat stat, SkillData skillData, int level = 0)
    {

    }

    public void End(ITotalStat stat, SkillData skillData, int level = 0)
    {

    }
}

public class SwordBurstExit : SkillExit
{
    public void Exit(ITotalStat stat, SkillData skillData, int level = 0)
    {
        Managers.Game._player.SetColActive("Katana");
    }
}
