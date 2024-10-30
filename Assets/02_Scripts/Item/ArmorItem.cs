using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorItem : EquipmentItem
{
    EquipMentUI _equippedUI;
    DataTableManager _dataTable = Managers.DataTable;

    public ArmorItem(ItemData data) : base(data)
    {
        var type = _dataTable._EquipeedItemData.Find(t => t.Type == data.Type);
        type.Type = ItemData.ItemType.Armor;
        _equippedUI.StatSum();
    }
}
