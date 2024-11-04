using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PotionItemData : ItemData
{
    [Serializable]
    public enum ValueType
    {
        NotUse,
        Recovery,
        Atk,
        Def,
    }

    public ValueType ValType;
    public int CoolTime;
    public int DurationTime;
    public int Value;
}