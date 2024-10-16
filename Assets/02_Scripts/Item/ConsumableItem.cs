using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableItem : CountableItem,IUsableItem
{
    bool _isUse = true;
    public ConsumableItem(ItemData data, int amount = 1) : base(data, amount)
    {
    }

    public virtual bool Use()
    {
        if (Data == null||_amount==0|| !_isUse) { return false; }
        _amount -= 1;
        ItemEffect();
        return true;
    }

    public virtual void ItemEffect() { 
    
    
    }
}
