using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SkillData
{

    [Serializable]
    public enum SkillTypes
    {
        Active = 1,
        Passive
    }

    [Serializable]
    public enum StatTypes
    {
        None,
        HP,
        HPRecovery,
        MP,
        MPRecovery,
        ATK,
        DEF,
    }

    public int ID;
    public string SkillName;
    public string TargetScript;
    public string IconPath;
    public SkillTypes SkillType;
    public StatTypes StatType;
    //public int SkillType;
    //public int StatType;
    public int StatValue;
    public int BaseDamage;
    public int DamageValue;
    public int UseingMP;
    public int NeedSkillPoint;
    public int MaxLevel;
}
