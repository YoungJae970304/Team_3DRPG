using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ItemProxy : MonoBehaviour,IItemDragAndDropAble
{
    private Action<IItemDragAndDropAble> _itemProxyAction;
    private void Awake()
    {
        
    }
    public void ItemInsert(IItemDragAndDropAble moveSlot)
    {
        _itemProxyAction?.Invoke(moveSlot);
        
    }

    public void SetProxy(Action<IItemDragAndDropAble> action) {
        _itemProxyAction = action;
    }


    public void NullTarget()
    {
        
    }

    public bool DragEnter(Image icon)
    {
        return false;
    }

    public void DragExit(Image icon)
    {
        
    }
}
