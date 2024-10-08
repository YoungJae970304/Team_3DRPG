using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PotionItemData : ItemData
{
    public ValueType ValType;

    public float CoolTime;

    public float DurationTime;
    public float Value;

    [Serializable]
    public enum ValueType
    {
        NotUse,
        Recovery,
        Atk,
        Def,
    }

    //포션 타입
    ValueType _valueType;
    //타입에 따라 회복또는 버프적용
    float _value;
    //아이템 쿨타임
    float _coolTime;
    //아이템 지속 시간
    float _durationTime;
}