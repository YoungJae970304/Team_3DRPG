using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillModule;


public class NoneSkillEnter : SkillEnter
{
    public void Enter(ITotalStat stat)
    {
    }
}

public class NoneSkillStay : SkillStay
{
    public void End(ITotalStat stat)
    {
    }

    public void Stay(ITotalStat stat)
    {
    }
}
public class NoneSkillExit : SkillExit
{
    public void Exit(ITotalStat stat)
    {
    }
}
public class NoneSkillPassive :SkillPassive
{
    public void Passive(ITotalStat stat)
    {
    }
}





