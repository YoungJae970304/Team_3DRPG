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
        _slotType = _inventory._currentType;
        _index = transform.GetSiblingIndex();
    }
    public void UpdateInfo()
    {
        Item = _itemManager.GetItem(_index, _inventory._currentType);
        _slotType = _inventory._currentType;
    }
    //아이템 드롭시
    public override void ItemInsert(IItemDragAndDropAble moveSlot)
    {
        if (!(moveSlot is ItemSlot)) { return; }
        if (moveSlot is QuickItemSlot) { return; }                              //퀵슬롯이거나 타입이 다르면 리턴
        if (moveSlot is ShopItemSlot) { (moveSlot as ShopItemSlot).BuyConfirm(this); return; }
        if (!_itemManager.Containtype(_slotType, (moveSlot as ItemSlot)._slotType)) { return; }
        base.ItemInsert(moveSlot);
        
    }
    public override void EqualSlot(ItemSlot moveSlot)
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
        _itemManager.SwitchItem(_index, ((InventorySlot)moveSlot)._index, moveSlot.Item.Data.Type);
    }
    public Inventory GetInventory() {
        return _itemManager;
    }

    public override void Setitem(Item item)
    {
        
        if (item == null)
        {
            _itemManager.Remove(_index, _slotType);
            base.Setitem(item);
        }
        else if (_itemManager.Containtype(_slotType, item.Data.Type))
        {
            _itemManager.Setitem(_index, item);
            base.Setitem(item);
        }
        else
        {
            _itemManager.InsertItem(item);
        }
    }

    public override bool MoveItem(ItemSlot moveSlot)
    {
        Item item = moveSlot.Item;
        Setitem(item);
        return true;
    }

    public override void RemoveItem()
    {
        _itemManager.Remove(_index, _slotType);
        base.RemoveItem();
        
    }

    public override void NullTarget()
    {
        base.NullTarget();

    }
    public override void UpdateSlotInfo()
    {
        base.UpdateSlotInfo();
        //여러개 가질 수 있는 아이템일때 남은 개수가 0이면 삭제
        if (Item != null && Item is CountableItem) {
            if ((Item as CountableItem)._amount == 0) {
                _itemManager.Remove(_index, _slotType);
                Item = null;
            }
        }
    }
}
