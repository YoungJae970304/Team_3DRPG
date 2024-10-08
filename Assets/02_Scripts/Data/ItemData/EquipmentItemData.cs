using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EquipmentItemData : ItemData
{
    public int HealthRegen;
    public int Mana;
    public int ManaRegen;
    public int Defense;
    public int Health;
    public int AttackPower;

    //공격력(무기_Curr)
    int _attackPower;
    //방어력(방어구_Curr)
    int _defense;
    //체력(방어구_Curr)
    int _health;
    //체력 리젠(악세서리_Curr)
    int _healthRegen;
    //마나(악세서리_Curr)
    int _mana;
    //마나 리젠(악세서리_Curr)
    int _manaRegen;
}
