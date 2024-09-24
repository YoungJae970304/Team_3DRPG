using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CountItemData : ItemData
{
    //갯수가 존재하는 아이템 데이터
    //기본적인 ItemData 상속
    public int MaxAmount => _maxAmount;
    public float CoolTime => _coolTime;
    //최대 소지 갯수
    [SerializeField] int _maxAmount = 99;
    //아이템 쿨타임
    [SerializeField] float _coolTime;
}
