using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSlot : ItemSlot
{
    
    public void ChangeEquipment() { 
    
    }
    public override void ItemInsert(ItemSlot moveSlot)
    {
        Item item = moveSlot.Item;
        if (item.Data.Type != slotType) { return; }
        moveSlot.MoveItem(this);
        Item = item;
        UpdateInfo();
    }

    public override bool MoveItem(ItemSlot moveSlot)
    {
        Item = moveSlot.Item;
        UpdateInfo();
        return true;
    }
}
