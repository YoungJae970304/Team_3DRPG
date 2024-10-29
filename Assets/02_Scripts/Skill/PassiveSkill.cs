using SkillModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveSkill : SkillBase
{
    public PassiveSkill(int skillId) : base(skillId)
    {
        Passive = new PassiveSkillEffect();
    }
}

public class PassiveSkillEffect : SkillPassive
{
    public void Passive(ITotalStat stat, SkillData skillData, int level = 0)
    {
        int curValue = skillData.StatValue * level;
        int beforeValue = skillData.StatValue * (level - 1);
        ApplyStat(stat, skillData.StatType, curValue, beforeValue);
    }


    public void ApplyStat(ITotalStat stat, SkillData.StatTypes statType, int curValue, int beforeValue)
    {
        switch (statType)
        {
            case SkillData.StatTypes.None:
                return;
            case SkillData.StatTypes.HP:
                stat.MaxHP = curValue;
                return;
            case SkillData.StatTypes.HPRecovery:
                stat.RecoveryHP = curValue;
                return;
            case SkillData.StatTypes.MP:
                stat.MaxMP = curValue;
                return;
            case SkillData.StatTypes.MPRecovery:
                stat.RecoveryMP = curValue;
                return;
            case SkillData.StatTypes.ATK:
                stat.ATK = curValue;
                return;
            case SkillData.StatTypes.DEF:
                stat.DEF = curValue;
                return;
            default:
                return;
        }
    }
}
