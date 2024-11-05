using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowOnlySlot : ItemSlot
{
    
    public override bool MoveItem(ItemSlot moveSlot)
    {
        return false;
    }

    public override void ItemInsert(IItemDragAndDropAble moveSlot)
    {
        
        return;
    }

    public override void UpdateSlotInfo()
    {
        base.UpdateSlotInfo();
        _isLocked = true;
        _isUsealbe = false;
    }

    public override void Setitem(Item item)
    {
        Item = item;
    }

}
