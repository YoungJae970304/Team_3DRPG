using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionItem : CountableItem, IUsableItem
{
    public PotionItem(PotionItemData data, int amount = 1) : base (data, amount) {}

    public bool Use()
    {
        _amount--;
        return true;
    }
}
