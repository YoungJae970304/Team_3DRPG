using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSlot : ItemSlot
{
    EquipMentUI _equipMentUI;

    public override void ItemInsert(ItemSlot moveSlot)
    {
        if (moveSlot is ShopItemSlot)//상점창이면 무시함
        { return; }
        Item item = moveSlot.Item;
        if (item.Data.Type != slotType) { return; }
        base.ItemInsert(moveSlot);
            
    }

    public override bool MoveItem(ItemSlot moveSlot)
    {
        Item = moveSlot.Item;
        return true;
    }
}
