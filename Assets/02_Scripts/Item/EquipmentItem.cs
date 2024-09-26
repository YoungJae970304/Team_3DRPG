using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentItem : Item
{

    public EquipmentItem(EquipmentItemData data) : base(data)
    {
    }


    //장비 아이템 복제
    protected override Item Clone(int amount)
    {
        return new EquipmentItem(Data as EquipmentItemData);
    }

    //장비 아이템이 사용할 함수
}
