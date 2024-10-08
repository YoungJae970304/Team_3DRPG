using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipMentUI : ItemUI
{
    private EquipmentItemData _weaponItem;
    private EquipmentItemData _armorItem;
    private EquipmentItemData _accessoriesItem;

    public EquipmentItemData WeaponItem { 
        get=> _weaponItem; 
        set {
            _weaponItem = value;
            StatSum(WeaponItem, ArmorItem, AccessoriesItem);
        } }
    public EquipmentItemData ArmorItem {
        get => _armorItem;
        set
        {
            _armorItem = value;
            StatSum(WeaponItem, ArmorItem, AccessoriesItem);
        }
    }
    public EquipmentItemData AccessoriesItem {
        get => _accessoriesItem;
        set
        {
            _accessoriesItem = value;
            StatSum(WeaponItem, ArmorItem, AccessoriesItem);
        }
    }

    PlayerStat _equipStat;
    public override void Init(Transform anchor)
    {
        base.Init(anchor);
        _equipStat = new PlayerStat();
        StatSum(WeaponItem, ArmorItem, AccessoriesItem);
    }
    public void StatSum(params EquipmentItemData[] items) {
        foreach (var item in items)
        {
            if (item == null) { continue; }
            _equipStat.RecoveryHP = item.HealthRegen;
            _equipStat.PlayerMaxMP = item.Mana;
            _equipStat.RecoveryMP = item.ManaRegen;
            _equipStat.DEF = item.Defense;
            _equipStat.MaxHP = item.Health;
            _equipStat.ATK = item.AttackPower;
        }
    }

}
