using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : EquipmentItem
{
    public WeaponItem(ItemData data) : base(data)
    {
    }

    public override void Equip(EquipMentUI equipMentUI)
    {
        base.Equip(equipMentUI);
        Data.Type = ItemData.ItemType.Weapon;
        Debug.Log($"Equipping Weapon: {Data.Name}");
    }
}
