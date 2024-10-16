using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableItem : CountableItem,IUsableItem
{
    public ConsumableItem(ItemData data, int amount = 1) : base(data, amount)
    {
    }

    public virtual bool Use()
    {
        if (Data == null||_amount==0) { return false; }
        _amount -= 1;
        if (_amount == 0) { 

        }
        return true;
    }
}
