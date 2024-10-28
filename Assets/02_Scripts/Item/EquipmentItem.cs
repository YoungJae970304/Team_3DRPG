using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentItem : Item
{
    public EquipmentItem(ItemData data) : base(data)
    {

    }

    public static bool CheckEquipmentType(ItemData.ItemType itemType)
    {
        return itemType >= ItemData.ItemType.Weapon && itemType <= ItemData.ItemType.Accessories;
    }
}
