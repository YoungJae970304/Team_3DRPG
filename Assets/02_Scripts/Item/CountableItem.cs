using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CountableItem : Item
{
    //수량이 있는 아이템에 적용
    //수량 있는 아이템 데이터 프로퍼티
    public ItemData _itemData { get; private set; }

    //현재 아이템 갯수
    public int _amount { get; protected set; }
    //최대로 소지할 수 있는 갯수
    public int _maxAmount => _itemData.MaxAmount;

    //수량이 가득 찼는지 여부
    public bool _isMax => _amount >= _itemData.MaxAmount;
    //개수가 없는지 여부
    public bool _isEmpty => _amount <= 0;

    //수량이 있는 아이템은 1개부터 시작하는 함수
    public CountableItem(ItemData data, int amount = 1) : base(data)
    {
        _itemData = data;
        SetAmount(amount);
        Logger.Log($"{_amount} 수량체크");
    }

    //최대 개수 지정
    public int SetAmount(int amount)
    {
        _amount = Mathf.Clamp(amount, 0, _maxAmount);

        return _amount;
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
        int over = 0;
        //추가된 수량이 최대개수(99) 보타 커지면
        if(nextAmount > _maxAmount)
        {
            over = nextAmount - _maxAmount;
            _amount = _maxAmount;
        }else
        {
            _amount = nextAmount;
        }
        return over;
    }

    //새로운 아이템 생성
    protected abstract CountableItem Clone(int amount);
}
