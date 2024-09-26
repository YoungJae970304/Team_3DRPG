using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountableItem : Item
{ //현재 아이템 갯수
    public int _amount { get; protected set; }
    //최대로 소지할 수 있는 갯수
    public virtual int _maxAmount => Data.MaxAmount;
   // public override int _maxAmount => Data.MaxAmount;
    //수량이 있는 아이템은 1개부터 시작하는 함수
    public CountableItem(ItemData data, int amount = 1) : base(data)
    {
        SetAmount(amount);
    }

    //장비 제외 다른 아이템 복사
    protected override Item Clone(int amount)
    {
        return new CountableItem(Data, amount);
    }
    //실제 현재 몇개인지 알려줄 함수
    public int GetCurrentAmount()
    {
        return _amount;
    }

    //현재 몇개인지 확인 해주는 함수
    public int AddAmount(int amount)
    {
        int nextAmount = _amount + amount;
        //현재 수량과 추가된 수량이 _maxAmount를 초과 했는지 확인 할 변수
        int overAmount = 0;
        //추가된 수량이 최대개수(99) 보다 커지면
        if (nextAmount > _maxAmount)
        {
            overAmount = nextAmount - _maxAmount;
            _amount = _maxAmount;
        }
        else
        {
            _amount = nextAmount;
        }
        return overAmount;
    }

    //최대 개수 99개로
    public virtual int SetAmount(int amount)
    {
        _amount = Mathf.Clamp(amount, 0, _maxAmount);
        return _amount;
    }
}
