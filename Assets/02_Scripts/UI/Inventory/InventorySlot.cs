using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : ItemSlot
{
    Inventory _itemManager;
    InventoryUI _inventory;
    public int _index;
    public virtual void Init(Inventory itemManager, InventoryUI inventory)
    {
        _itemManager = itemManager;
        _inventory = inventory;
        slotType = _inventory._currentType;
        _index = transform.GetSiblingIndex();
    }
    public void UpdateInfo()
    {
        Item = _itemManager.GetItem(_index, _inventory._currentType);
        slotType = _inventory._currentType;
    }
    public override void ItemInsert(ItemSlot moveSlot)
    {
        if (!_itemManager.Containtype(slotType, moveSlot.slotType)) { return; }
        if (moveSlot is InventorySlot) {
            _itemManager.SwitchItem(_index, ((InventorySlot)moveSlot)._index, moveSlot.Item.Data.Type);
        }
        else
        {
            Item item = moveSlot.Item;
            moveSlot.MoveItem(this);
            _itemManager.Setitem(_index, item);
        }
        
    }
    public override bool MoveItem(ItemSlot moveSlot)
    {
        Item item = moveSlot.Item;

        if (item == null)
        {
            _itemManager.Remove(_index, slotType);
        }
        else {
            _itemManager.Setitem(_index, item);
        }
        
        return true;
    }
}
