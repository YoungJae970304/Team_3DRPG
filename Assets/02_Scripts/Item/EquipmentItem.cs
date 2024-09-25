using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentItem : Item
{
    //장비 아이템 최대 1개
    public override int _maxAmount => 1;

    //장비 아이템은 1개가 최대치로 설정 해줄 함수
    public EquipmentItem(EquipmentItemData data) : base(data, 1)
    {
        SetAmount(1);
    }

    //장비 아이템 최대 갯수 1개로 설정
    public override int SetAmount(int amount)
    {
        return base.SetAmount(1);
    }

    //장비 아이템 복제
    protected override Item Clone(int amount)
    {
        return new EquipmentItem(Data as EquipmentItemData);
    }

    //장비 아이템이 사용할 함수들이 이제 플레이어가 장비를 장착하거나 뺏을 때
    //플레이어 스텟에서 더해주거나 빼줘야함
}
