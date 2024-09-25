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
    //소비 아이템은 사용 시 수량을 제거해주고 제거 한 아이템 Type이 포션 그 타입의 id를 확인해서 회복 포션, 버프 포션 구분
    //플레이어 스텟에서 체력을 회복할지 버프를 줄지 결정
    //결정 후 회복 또는 버프아이템 효과 적용
    //현재 테스트해볼 곳이 아이템프리팹이 몬스터를 처치하였을 때 아이템 프리팹을 생성시켜줘야함.
    //아이템 프리팹은 각 필요한 스크립터블 오브젝트를 갖고 있게 해주어 테스트 해봐야함
}
