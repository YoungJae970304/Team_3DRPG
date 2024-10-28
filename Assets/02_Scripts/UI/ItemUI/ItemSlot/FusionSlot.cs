using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class FusionSlot : ItemSlot
{
    public Inventory _inventory;
    public override void ItemInsert(IItemDragAndDropAble moveSlot)
    {
        if (!(moveSlot is InventorySlot)) { return; }
        base.ItemInsert(moveSlot);
    }

    public override bool DragEnter(Image icon)
    {
        return base.DragEnter(icon);
    }
    public override bool MoveItem(ItemSlot moveSlot)
    {
        if (moveSlot.GetType() == GetType())
        {
            Item = moveSlot.Item;
            return true;
        }
        return false;
    }
    public override void NullTarget()
    {
        _inventory.InsertItem(Item);
        Item = null;
        
    }
}
;



