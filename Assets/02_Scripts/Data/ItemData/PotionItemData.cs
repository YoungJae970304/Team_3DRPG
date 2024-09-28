using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionItemData : ItemData
{
    //포션 아이템 데이터를 생성
    public float Value => _value;
    public float CoolTime => _coolTime;
    //회복량(효과 - 버프등)
    [SerializeField] float _value;
    //아이템 쿨타임
    [SerializeField] float _coolTime;
}