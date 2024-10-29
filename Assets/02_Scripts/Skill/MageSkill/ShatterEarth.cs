using Data;
using SkillModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShatterEarth: SkillBase
{
    private const int SKILL_ID = 11;
    public ShatterEarth() : base(SKILL_ID)
    {
        Enter = new ShatterEarthEnter();
        Stay = new ShatterEarthStay();
        Exit = new ShatterEarthExit();
        Passive = new NoneSkillPassive();

        skillType = Define.SkillType.Normal;
        delay = 2f;
    }
}

public class ShatterEarthEnter : SkillEnter
{
    public void Enter(ITotalStat stat, SkillData skillData, int level = 0)
    {
        Managers.Game._player._playerAnim.Play("Skill2");

        // 전방에 범위 데미지
    }
}

public class ShatterEarthStay : SkillStay
{
    public void Stay(ITotalStat stat, SkillData skillData, int level = 0)
    {

    }

    public void End(ITotalStat stat, SkillData skillData, int level = 0)
    {

    }
}

public class ShatterEarthExit : SkillExit
{
    public void Exit(ITotalStat stat, SkillData skillData, int level = 0)
    {

    }
}
