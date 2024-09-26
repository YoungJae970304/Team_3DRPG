using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item
{
    public ItemData Data { get; private set; }

   
    
    public Item(ItemData data)
    {
        Data = data;
    }
    //새로운 아이템 생성
    protected abstract Item Clone(int amount);
}
