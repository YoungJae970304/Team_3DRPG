using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class EquipmentItem : Item
{
    public EquipmentItem(ItemData data) : base(data)
    {

    }

    public virtual void Equip(EquipMentUI equipMentUI)
    {
        // 기본 장비 장착 로직
        equipMentUI.StatSum();
    }
}
