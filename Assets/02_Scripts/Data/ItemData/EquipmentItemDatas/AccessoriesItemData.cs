using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessoriesItemData : EquipmentItemData
{
    public int HealthRegen => _healthRegen;
    public int Mana => _mana;
    public int ManaRegen => _manaRegen;

    //체력 리젠(악세서리)
    [SerializeField] int _healthRegen;
    //마나(악세서리)
    [SerializeField] int _mana;
    //마나 리젠(악세서리)
    [SerializeField] int _manaRegen;
}
