using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EquipmentItem : Item
{
    public EquipmentItem(ItemData data) : base(data)
    {

    }

    public virtual void Equip(EquipMentUI equipMentUI)
    {
        equipMentUI.StatSum();
    }
}
