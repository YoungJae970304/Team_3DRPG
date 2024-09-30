using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessoriesItemData : EquipmentItemData
{
    public int HealthRegen { get { return _healthRegen; } set { _healthRegen = value; } }
    public int Mana { get { return _mana; } set { _mana = value; } }
    public int ManaRegen { get { return _manaRegen; } set { _manaRegen = value; } }

    //체력 리젠(악세서리)
    int _healthRegen;
    //마나(악세서리)
    int _mana;
    //마나 리젠(악세서리)
    int _manaRegen;
}
