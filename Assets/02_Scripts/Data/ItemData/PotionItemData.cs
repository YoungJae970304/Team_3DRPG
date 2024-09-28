using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionItemData : ItemData
{

    public enum ValueType
    {
        NotUse,
        Hp,
        Mp,
        Atk,
        Def,
    }

    //회복량(효과 - 버프등)
    public ValueType _valueType;
    //아이템 쿨타임
    public float _coolTime;
    //아이템 지속 시간
    public float _durationTime;
}