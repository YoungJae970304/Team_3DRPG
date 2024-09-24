using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CountableItem : Item
{
    //수량이 있는 아이템에 적용
    //수량 있는 아이템 데이터 프로퍼티
    public CountableItemData _countItemData { get; private set; }

    //현재 아이템 갯수
    public int _amount { get; protected set; }
    //최대로 소지할 수 있는 갯수
    public int _maxAmount => _countItemData.MaxAmount;

    //수량이 가득 찼는지 여부
    public bool _isMax => _amount >= _countItemData.MaxAmount;
    //개수가 없는지 여부
    public bool _isEmpty => _amount <= 0;

    public CountableItem(CountableItemData data, int amount = 1) : base(data)
    {
        _countItemData = data;
        SetAmount(amount);
    }

    //최대 갯수 지정
    public void SetAmount(int amount)
    {
        _amount = Mathf.Clamp(amount, 0, _maxAmount);
    }

    //99개가 넘어갔을 때 (함수 호출 하고)
    //인벤토리 안에 있는 꽉찬 아이템 슬롯(idx?) 다음 아이템 슬롯(idx?)에서 부터 다시 CountableItem함수 호출

    //갯수가 0 될 시 (함수 호출) 인벤토리에서 빼주고 인벤토리 갱신

}
