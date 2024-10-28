using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : EquipmentItem
{
    EquipMentUI _equippedUI;
    DataTableManager _dataTable = Managers.DataTable;

    public WeaponItem(ItemData data) : base(data)
    {
        var type = _dataTable._EquipeedItemData.Find(t => t.Type == data.Type);
        type.Type = ItemData.ItemType.Weapon;
        _equippedUI.StatSum();
    }
}
