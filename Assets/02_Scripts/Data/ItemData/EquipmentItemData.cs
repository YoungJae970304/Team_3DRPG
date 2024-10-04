using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentItemData : ItemData
{
    public int HealthRegen { get { return _healthRegen; } set { _healthRegen = value; } }
    public int Mana { get { return _mana; } set { _mana = value; } }
    public int ManaRegen { get { return _manaRegen; } set { _manaRegen = value; } }
    public int Defense { get { return _defense; } set { _defense = value; } }
    public int Health { get { return _health; } set { _health = value; } }
    public int AttackPower { get { return _attackPower; } set { _attackPower = value; } }

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
