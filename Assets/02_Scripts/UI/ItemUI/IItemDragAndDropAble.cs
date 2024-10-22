using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IItemDragAndDropAble 
{
    //아이템 드롭시 호출
    public void ItemInsert(IItemDragAndDropAble moveSlot);
    public void NullTarget();
    public bool DragEnter(Image icon );
    public void DragExit(Image icon);
}
