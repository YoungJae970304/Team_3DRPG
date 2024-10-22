using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSlot : ItemSlot
{
    EquipMentUI _equipMentUI;

    public override void ItemInsert(IItemDragAndDropAble moveSlot)
    {
        if (!(moveSlot is ItemSlot)) { return; }
        if (moveSlot is ShopItemSlot)//상점창이면 무시함
        { return; }
        Item item = (moveSlot as ItemSlot).Item;
        if (item.Data.Type != _slotType) { return; }
        base.ItemInsert(moveSlot);
            
    }

    public override bool MoveItem(ItemSlot moveSlot)
    {
        Item = moveSlot.Item;
        return true;
    }
}
