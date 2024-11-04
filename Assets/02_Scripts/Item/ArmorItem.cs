using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorItem : EquipmentItem
{
    public ArmorItem(ItemData data) : base(data)
    {
    }

    public override void Equip(EquipMentUI equipMentUI)
    {
        base.Equip(equipMentUI);
        Data.Type = ItemData.ItemType.Armor;
        Debug.Log($"Equipping Armor: {Data.Name}");
    }
}
