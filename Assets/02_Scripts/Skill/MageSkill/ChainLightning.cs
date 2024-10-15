using SkillModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainLightning : SkillBase
{
    public override SkillEnter Enter { get; set; } = new ChainLightningEnter();
    public override SkillStay Stay { get; set; } = new ChainLightningStay();
    public override SkillExit Exit { get; set; } = new ChainLightningExit();
    public override SkillPassive Passive { get; set; } = new ChainLightningPassive();

    public ChainLightning()
    {
        skillType = Define.SkillType.Normal;
        delay = 2f;
    }
}

public class ChainLightningEnter : SkillEnter
{
    public void Enter(ITotalStat stat)
    {
        Managers.Game._player._playerAnim.Play("Skill3");
    }
}

public class ChainLightningStay : SkillStay
{
    public void Stay(ITotalStat stat)
    {

    }

    public void End(ITotalStat stat)
    {

    }
}

public class ChainLightningExit : SkillExit
{
    public void Exit(ITotalStat stat)
    {

    }
}

public class ChainLightningPassive : SkillPassive
{
    public void Passive(ITotalStat stat)
    {
        Debug.Log("TestSkill 패시브 효과");

        PlayerStatManager pStat = (PlayerStatManager)stat;
        pStat._buffStat.MaxHP += 50;
        pStat._buffStat.ATK += 30;
    }
}
