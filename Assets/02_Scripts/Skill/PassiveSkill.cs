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
    public void Passive(ITotalStat stat, SkillData skillData, int curLevel, int prevLevel)
    {
        int curValue = skillData.StatValue * curLevel;
        int prevValue = skillData.StatValue * prevLevel;
        int difference = curValue - prevValue;

        ApplyStat(stat, skillData.StatType, difference);
    }


    public void ApplyStat(ITotalStat stat, SkillData.StatTypes statType, int diff)
    {
        switch (statType)
        {
            case SkillData.StatTypes.None:
                return;
            case SkillData.StatTypes.HP:
                stat.MaxHP = diff;
                Logger.LogWarning($"MaxHP 증가량 {diff}, 현재 MaxHP {stat.MaxHP}");
                return;
            case SkillData.StatTypes.HPRecovery:
                stat.RecoveryHP = diff;
                Logger.LogWarning($"RecoveryHP 증가량 {diff}, 현재 RecoveryHP {stat.RecoveryHP}");
                return;
            case SkillData.StatTypes.MP:
                stat.MaxMP = diff;
                Logger.LogWarning($"MaxMP 증가량 {diff}, 현재 MaxMP {stat.MaxMP}");
                return;
            case SkillData.StatTypes.MPRecovery:
                stat.RecoveryMP = diff;
                Logger.LogWarning($"RecoveryMP 증가량 {diff}, 현재 RecoveryMP {stat.RecoveryMP}");
                return;
            case SkillData.StatTypes.ATK:
                stat.ATK = diff;
                Logger.LogWarning($"ATK 증가량 {diff}, 현재 ATK {stat.ATK}");
                return;
            case SkillData.StatTypes.DEF:
                stat.DEF = diff;
                Logger.LogWarning($"DEF 증가량 {diff}, 현재 DEF {stat.DEF}");
                return;
            default:
                return;
        }
    }
}
