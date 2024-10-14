using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemProxy : MonoBehaviour,IItemDropAble
{
    private Action<ItemSlot> _itemProxyAction;
    private void Awake()
    {
        
    }
    public void ItemInsert(ItemSlot moveSlot)
    {
        _itemProxyAction?.Invoke(moveSlot);
        
    }

    public void SetProxy(Action<ItemSlot> action) {
        _itemProxyAction = action;
    }

}
