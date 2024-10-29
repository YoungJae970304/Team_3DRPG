using SkillModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainLightning : SkillBase
{
    private const int SKILL_ID = 12;

    public ChainLightning() : base(SKILL_ID)
    {
        Enter = new ChainLightningEnter();
        Stay = new ChainLightningStay();
        Exit = new ChainLightningExit();
        Passive = new NoneSkillPassive();
    }
}

public class ChainLightningEnter : SkillEnter
{
    public void Enter(ITotalStat stat, SkillData skillData, int level = 0)
    {
        Managers.Game._player._playerAnim.Play("Skill3");
    }
}

public class ChainLightningStay : SkillStay
{
    public void Stay(ITotalStat stat,SkillData skillData, int level=0)
    {
        
    }

    public void End(ITotalStat stat, SkillData skillData, int level = 0)
    {

    }
}

public class ChainLightningExit : SkillExit
{
    public void Exit(ITotalStat stat, SkillData skillData, int level = 0)
    {

    }
}

//public class ChainLightningPassive : SkillPassive
//{
//    public void Passive(ITotalStat stat, SkillData skillData, int level = 0)
//    {
//        Debug.Log("TestSkill 패시브 효과");

//        stat.MaxHP = 50;
//        stat.ATK = 30;
//    }
//}
