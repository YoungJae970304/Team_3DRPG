using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillModule;


public class NoneSkillEnter : SkillEnter
{
    public void Enter(ITotalStat stat, int level = 0)
    {
    }
}

public class NoneSkillStay : SkillStay
{
    public void End(ITotalStat stat, int level = 0)
    {
    }

    public void Stay(ITotalStat stat, int level = 0)
    {
    }
}
public class NoneSkillExit : SkillExit
{
    public void Exit(ITotalStat stat, int level = 0)
    {
    }
}
public class NoneSkillPassive :SkillPassive
{
    public void Passive(ITotalStat stat, int level = 0)
    {
    }
}





