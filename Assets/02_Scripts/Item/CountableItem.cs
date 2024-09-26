using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountableItem : Item
{
    public override int _maxAmount => Data.MaxAmount;
    //수량이 있는 아이템은 1개부터 시작하는 함수
    public CountableItem(ItemData data, int amount = 1) : base(data, amount)
    {
        SetAmount(amount);
    }

    //장비 제외 다른 아이템 복사
    protected override Item Clone(int amount)
    {
        return new CountableItem(Data, amount);
    }
}
