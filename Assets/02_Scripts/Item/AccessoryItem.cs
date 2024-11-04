using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessoryItem : EquipmentItem
{
    public AccessoryItem(ItemData data) : base(data)
    {
   
    }

    public override void Equip(EquipMentUI equipMentUI)
    {
        base.Equip(equipMentUI);
        Data.Type = ItemData.ItemType.Accessories;
        Debug.Log($"Equipping Accessory: {Data.Name}");
    }
}
