using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillModule;


public class NoneSkillEnter : SkillEnter
{
    public void Enter(ITotalStat stat, SkillData skillData, int level = 0)
    {
    }
}

public class NoneSkillStay : SkillStay
{
    public void End(ITotalStat stat, SkillData skillData, int level = 0)
    {
    }

    public void Stay(ITotalStat stat, SkillData skillData, int level = 0)
    {
    }
}
public class NoneSkillExit : SkillExit
{
    public void Exit(ITotalStat stat, SkillData skillData, int level = 0)
    {
    }
}
public class NoneSkillPassive : SkillPassive
{
    public void Passive(ITotalStat stat, SkillData skillData, int curLevel, int prevLevel)
    {
        
    }
}





