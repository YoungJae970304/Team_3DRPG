using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessoryItem : EquipmentItem
{
    EquipMentUI _equippedUI;
    DataTableManager _dataTable = Managers.DataTable;

    public AccessoryItem(ItemData data) : base(data)
    {
        var type = _dataTable._EquipeedItemData.Find(t => t.Type == data.Type);
        type.Type = ItemData.ItemType.Accessories;
        _equippedUI.StatSum();
    }
}
