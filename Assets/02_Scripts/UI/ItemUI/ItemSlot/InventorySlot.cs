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
        if (Item != null && Item.Data.ID == moveSlot.Item.Data.ID)
        {
            if (Item is CountableItem)
            {
                int overAmount = ((CountableItem)Item).AddAmount(((CountableItem)moveSlot.Item)._amount);
                ((CountableItem)moveSlot.Item).SetAmount(overAmount);
                UpdateSlotInfo();
                moveSlot.UpdateSlotInfo();
                return;
            }
        }
        if (!_itemManager.Containtype(slotType, moveSlot.slotType)) { return; }
        if (moveSlot is InventorySlot) {
            _itemManager.SwitchItem(_index, ((InventorySlot)moveSlot)._index, moveSlot.Item.Data.Type);
        }
        else if (moveSlot is ShopItemSlot) {
            (moveSlot as ShopItemSlot).BuyConfirm(this);
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
        else if (moveSlot.slotType == slotType)
        {
            _itemManager.Setitem(_index, item);
        }
        else {
            _itemManager.InsertItem(item);
        }
        
        return true;
    }

    public override void UpdateSlotInfo()
    {
        base.UpdateSlotInfo();
        //여러개 가질 수 있는 아이템일때 남은 개수가 0이면 삭제
        if (Item != null && Item is CountableItem) {
            if ((Item as CountableItem)._amount == 0) {
                _itemManager.Remove(_index, slotType);
                Item = null;
            }
        }
    }
}
