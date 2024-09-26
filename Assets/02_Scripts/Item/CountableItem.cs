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
    //소비 아이템은 사용 시 수량을 제거해주고 제거 한 아이템 Type이 포션 그 타입의 id를 확인해서 회복 포션, 버프 포션 구분
    //플레이어 스텟에서 체력을 회복할지 버프를 줄지 결정
    //결정 후 회복 또는 버프아이템 효과 적용
    //현재 테스트해볼 곳이 아이템프리팹이 몬스터를 처치하였을 때 아이템 프리팹을 생성시켜줘야함.
    //아이템 프리팹은 각 필요한 스크립터블 오브젝트를 갖고 있게 해주어 테스트 해봐야함

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
