using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentItem : Item
{

    public EquipmentItem(ItemData data) : base(data)
    {
        //Logger.Log(data.Name);
    }


    //장비 아이템 복제
    //protected override Item Clone(int amount)
    //{
    //    return new EquipmentItem(Data as EquipmentItemData);
    //}
}
