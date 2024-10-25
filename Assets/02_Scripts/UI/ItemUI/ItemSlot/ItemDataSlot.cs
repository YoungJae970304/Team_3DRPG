using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataSlot : ItemSlot
{
    public override void ItemInsert(IItemDragAndDropAble moveSlot)
    {
        if (!(moveSlot is InventorySlot))//인벤토리가 아니면 무시함
        { return; }
        Item item = (moveSlot as InventorySlot).Item;
        _slotType = item.Data.Type;
        Item = item;
    }
    public override bool MoveItem(ItemSlot moveSlot)
    {
        return false;
    }
}
