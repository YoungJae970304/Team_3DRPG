using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItemDropAble 
{
    //아이템 드롭시 호출
    public void ItemInsert(ItemSlot moveSlot);
}
public interface IItemDragAble
{
    public void NullTarget();
}
