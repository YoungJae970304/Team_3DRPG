using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableItem : CountableItem,IUsableItem
{
    bool _isUse = true;
    public ConsumableItem(ItemData data, int amount = 1) : base(data, amount)
    {
    }

    public virtual bool Use(IDamageAlbe target)
    {
        if (Data == null||_amount==0|| !_isUse||target == null) { return false; }
        _amount -= 1;
        ItemEffect(target);
        return true;
    }

    public virtual void ItemEffect(IDamageAlbe target) {
        if (Data is PotionItemData) {
            PotionItemData potionData = Data as PotionItemData;
            switch (potionData.ValType) {
                case PotionItemData.ValueType.NotUse:
                    return;
                case PotionItemData.ValueType.Recovery:
                    target.StatusEffect.SpawnEffect<HealEffect>(potionData.DurationTime, (int)potionData.Value);
                    break;
                case PotionItemData.ValueType.Atk:
                    target.StatusEffect.SpawnEffect<ATKBuffEffect>(potionData.DurationTime, (int)potionData.Value);
                    break;
                case PotionItemData.ValueType.Def:
                    target.StatusEffect.SpawnEffect<DEFBuffEffect>(potionData.DurationTime, (int)potionData.Value);
                    break;
            }
        }
    }
}
