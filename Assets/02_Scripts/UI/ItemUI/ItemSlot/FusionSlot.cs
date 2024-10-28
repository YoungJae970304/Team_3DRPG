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
        
        //base.ItemInsert(moveSlot);
        if (moveSlot.GetType() == GetType())//같은종류의 슬롯이면
        {
            EqualSlot(moveSlot as ItemSlot);
        }
        else {
            if (!(moveSlot is InventorySlot)) { return; }
            InventorySlot moveitemSlot = moveSlot as InventorySlot;
            Item item = moveitemSlot.Item;
            if (item != null)
            {

                moveitemSlot.Setitem(null);
                if (Item != null) {
                    _inventory.InsertItem(Item);
                }
                
                Setitem(item);
            }
        }
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



