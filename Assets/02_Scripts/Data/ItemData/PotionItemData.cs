using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionItemData : ItemData
{
    public ValueType ValType { get { return _valueType; } set { _valueType = value; } }

    public float CoolTime { get { return _coolTime; }set { _coolTime = value; } }

    public float DurationTime { get { return _durationTime; } set { _durationTime = value; } }
    public float Value { get { return _value; } set { _value = value; } }

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